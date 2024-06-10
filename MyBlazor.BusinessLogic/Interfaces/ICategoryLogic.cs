using MyBlazor.DataAccess.Entities;

namespace MyBlazor.BusinessLogic.Interfaces;

public interface ICategoryLogic
{
    IEnumerable<Category> GetAllAsync();
    Task<Category> GetAsync(int id);
    Task CreateAsync(Category item);
    Task UpdateAsync(Category item);
    Task<bool> DeleteAsync(int id);
}