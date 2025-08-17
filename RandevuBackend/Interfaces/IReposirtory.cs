namespace RandevuBackend.Interfaces;

public interface IReposirtory<T> where T: class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task <T> GetFromIdAscyn(int id);
    Task<T> AddAsync();
}