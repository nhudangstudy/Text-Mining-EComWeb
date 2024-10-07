namespace Common.IRepositories
{
    public interface IScopeRepository
    {
        IAsyncEnumerable<string> GetAllById(IEnumerable<int> ids);
    }
}
