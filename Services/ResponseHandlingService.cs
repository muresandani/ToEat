using ChatChoice = Azure.AI.OpenAI.ChatChoice;
using ToEat.Strategies;

namespace ToEat.Services;
public class ResponseHandlingService
{
    private readonly IEnumerable<IResponseHandlingStrategy> _strategies;

    public ResponseHandlingService(IEnumerable<IResponseHandlingStrategy> strategies)
    {
        _strategies = strategies;
    }

    public void HandleResponse(ChatChoice response)
    {

        Azure.AI.OpenAI.CompletionsFinishReason? finishReason = response.FinishReason;
        if (!finishReason.HasValue)
        {
            throw new Exception($"No finish reason found");
        }
        var strategy = _strategies.FirstOrDefault(s => s.CanHandle(finishReason.ToString()));
        
        if (strategy == null)
        {
            throw new Exception($"No strategy found for response type {finishReason.ToString()}");
        }
        strategy.Handle(response);
    }
}