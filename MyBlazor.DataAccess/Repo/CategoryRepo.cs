using MyBlazor.DataAccess.Context;
using MyBlazor.DataAccess.Entities;
using MyBlazor.DataAccess.Interfaces;

namespace MyBlazor.DataAccess.Repo;

public class CategoryRepo : BaseRepo, ICategoryRepo
{
    public CategoryRepo(MyContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<Category> GetAllAsync()
    {
        return myContext.Categories;
    }

    public async Task<Category> GetAsync(int id)
    {
        var category = await myContext.Categories.FindAsync(id);
        return category;
    }

    public async Task CreateAsync(Category category)
    {
        await myContext.Categories.AddAsync(category);
        await myContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        myContext.Categories.Update(category);
        await myContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await myContext.Categories.FindAsync(id);

        myContext.Categories.Remove(category);

        await myContext.SaveChangesAsync();

        return true;
    }
}