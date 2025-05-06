namespace Company.Function.Contracts;
public interface IXboxGameService
{
    Task<int> GetTotalGamesAsync();
}