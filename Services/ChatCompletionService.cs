using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;
using System.Text;

using UserConversation = ToEat.Models.Conversation;
using ToEat.Extensions;
using ToEat.Strategies;
using ToEat.Functions; 

namespace ToEat.Services;
public class ChatCompletionService
{
    private readonly OpenAIClient _openAIClient;
    private readonly ResponseHandlingService _responseHandlingService;

    private readonly FunctionRepository _functionRepository;
    public ChatCompletionService(IConfiguration configuration, ResponseHandlingService responseHandlingService, FunctionRepository functionRepository)
    {
        _responseHandlingService = responseHandlingService;
        _openAIClient = new OpenAIClient(configuration["OpenAI:ApiKey"], new OpenAIClientOptions());
        _functionRepository = functionRepository;
    }

    public async Task<string> GetAnswer(UserConversation userConversation)
    {
        var chatCompletionsOptions = new ChatCompletionsOptions()
        {
            DeploymentName = "gpt-4-1106-preview", 
        };

        foreach (var message in userConversation.ToOpenAiConversation())
        {
            chatCompletionsOptions.Messages.Add(message);
        }

        foreach (var function in _functionRepository.GetFunctions())
        {
            chatCompletionsOptions.Functions.Add(function.GetFunctionDefinition());
        }

        Response<ChatCompletions> response = _openAIClient.GetChatCompletions(chatCompletionsOptions);
        ChatCompletions chatCompletions = response.Value;

        IReadOnlyList<ChatChoice> choices = chatCompletions.Choices;
        Azure.AI.OpenAI.CompletionsFinishReason? test = choices.First().FinishReason;
        _responseHandlingService.HandleResponse(choices.First());
        StringBuilder allResponses = new StringBuilder();
        return  allResponses.ToString();
    }
}
