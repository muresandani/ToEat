using Azure.AI.OpenAI;
namespace ToEat.Strategies;
public interface IResponseHandlingStrategy
{
    bool CanHandle(string responseType);
    void Handle(ChatChoice response);
}
