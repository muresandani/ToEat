namespace ToEat.Strategies;
using Azure.AI.OpenAI;

public class SimpleAnswerStrategy : IResponseHandlingStrategy
{
    public bool CanHandle(string responseType)
    {
        return responseType == "stop";
    }
    public void Handle(ChatChoice response)
    {
        Console.WriteLine(response.Message);
    }
}