using Azure.AI.OpenAI;
using ToEat.Functions.Parameters;

namespace ToEat.Functions;

public interface IFunction
{
    void Execute(IFunctionArgument parameter);
    FunctionDefinition GetFunctionDefinition();
}
