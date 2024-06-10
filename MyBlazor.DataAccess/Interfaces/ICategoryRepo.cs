using MyBlazor.DataAccess.Entities;

namespace MyBlazor.DataAccess.Interfaces;

public interface ICategoryRepo
{
    IEnumerable<Category> GetAllAsync();
    Task<Category> GetAsync(int id);
    Task CreateAsync(Category item);
    Task UpdateAsync(Category item);
    Task<bool> DeleteAsync(int id);
}