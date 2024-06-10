using Microsoft.Extensions.Logging;
using MyBlazor.BusinessLogic.Interfaces;
using MyBlazor.DataAccess.Entities;
using MyBlazor.DataAccess.Interfaces;

namespace MyBlazor.BusinessLogic.Logic;

public class CategoryLogic : BaseLogic, ICategoryLogic
{
    private readonly ICategoryRepo _repo;
    
    public CategoryLogic(ILogger<BaseLogic> logger, ICategoryRepo repo) : base(logger)
    {
        _repo = repo;
    }


    public IEnumerable<Category> GetAllAsync()
    {
        try
        {
            Logger.LogInformation("Trying get all categories");
            var categories = _repo.GetAllAsync();
            Logger.LogInformation("Complete getting categories");
            
            return categories;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while getting categories");
            throw;
        }
    }

    public async Task<Category> GetAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying get category: {Id}", id);
            var category = await _repo.GetAsync(id);
            Logger.LogInformation("Complete getting category: {Id}", id);
            
            return category;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while getting category: {Id}", id);
            throw;
        }
    }

    public async Task CreateAsync(Category category)
    {
        try
        {
            Logger.LogInformation("Trying add category: {Id}", category.Id);
            await _repo.CreateAsync(category);
            Logger.LogInformation("Complete adding category: {Id}", category.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while adding category: {Id}", category.Id);
            throw;
        }
    }

    public async Task UpdateAsync(Category category)
    {
        try
        {
            Logger.LogInformation("Trying update category: {Id}", category.Id);
            await _repo.UpdateAsync(category);
            Logger.LogInformation("Complete updating category: {Id}", category.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while updating category: {Id}", category.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying delete category: {Id}", id);
            var res = await _repo.DeleteAsync(id);
            Logger.LogInformation("Complete deleting category: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while deleting category: {Id}", id);
            throw;
        }
    }
}