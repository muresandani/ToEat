namespace ToEat.Strategies;
using Azure.AI.OpenAI;
using ToEat.Functions.Parameters;
using ToEat.Functions;
public class FunctionCallStrategy : IResponseHandlingStrategy
{
    private readonly FunctionRepository _functionRepository;
    public FunctionCallStrategy(FunctionRepository functionRepository)
    {
        _functionRepository = functionRepository;
    }
    
    public bool CanHandle(string responseType)
    {
        return responseType == "function_call";
    }
    public void Handle(ChatChoice response)
    {
        var functionCall = response.Message.FunctionCall;
        string functionName = functionCall.Name;
        FunctionArgument parameters = new FunctionArgument(functionCall.Arguments);
        _functionRepository.GetFunction(functionName).Execute(parameters);
    }
}
