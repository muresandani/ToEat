using Azure.AI.OpenAI;
using ToEat.Functions.Parameters;
using ToEat.Functions.Responses;

namespace ToEat.Functions;

public interface IFunction
{
    IFunctionResponse Execute(IFunctionArgument parameter);
    FunctionDefinition GetFunctionDefinition();
}
