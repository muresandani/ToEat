using ToEat.Strategies;

public class StrategyRepository
{
    private readonly IEnumerable<IResponseHandlingStrategy> _strategies;

    public StrategyRepository(IEnumerable<IResponseHandlingStrategy> strategies)
    {
        _strategies = strategies;
    }
}