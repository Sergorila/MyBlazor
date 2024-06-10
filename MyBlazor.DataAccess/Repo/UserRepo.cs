using Microsoft.EntityFrameworkCore;
using MyBlazor.DataAccess.Context;
using MyBlazor.DataAccess.Entities;
using MyBlazor.DataAccess.Interfaces;

namespace MyBlazor.DataAccess.Repo;

public class UserRepo : BaseRepo, IUserRepo
{
    public UserRepo(MyContext dbContext) : base(dbContext) { }

    public IEnumerable<User> GetAllAsync()
    {
        return myContext.Users;
    }

    public async Task<User> GetAsync(int id)
    {
        var user = await myContext.Users.FindAsync(id);
        return user;
    }

    public async Task CreateAsync(User user)
    {
        await myContext.Users.AddAsync(user);
        await myContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        myContext.Users.Update(user);
        await myContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await myContext.Users.FindAsync(id);

        myContext.Users.Remove(user);

        await myContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CheckUserAsync(string login, string password)
    {
        var user = await myContext.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);

        return user != null;
    }
}