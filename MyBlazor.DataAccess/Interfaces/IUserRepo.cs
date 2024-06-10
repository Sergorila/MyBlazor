using MyBlazor.DataAccess.Entities;

namespace MyBlazor.DataAccess.Interfaces;

public interface IUserRepo
{
    IEnumerable<User> GetAllAsync();
    Task<User> GetAsync(int id);
    Task CreateAsync(User item);
    Task UpdateAsync(User item);
    Task<bool> DeleteAsync(int id);
    Task<bool> CheckUserAsync(string login, string password);
}