using Microsoft.EntityFrameworkCore;
using MyBlazor.DataAccess.Context;
using MyBlazor.DataAccess.Entities;
using MyBlazor.DataAccess.Interfaces;

namespace MyBlazor.DataAccess.Repo;

public class GameRepo: BaseRepo, IGameRepo
{
    public GameRepo(MyContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<Game> GetAllAsync()
    {
        return myContext.Games.Include(o => o.Orders).ToList();
    }

    public async Task<Game> GetAsync(int id)
    {
        var game = await myContext.Games.Include(o => o.Orders).Where(g => g.Id == id).FirstOrDefaultAsync();
        return game;
    }

    public async Task CreateAsync(Game game)
    {
        await myContext.Games.AddAsync(game);
        await myContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game game)
    {
        myContext.Games.Update(game);
        await myContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var game = await myContext.Games.FindAsync(id);

        myContext.Games.Remove(game);

        await myContext.SaveChangesAsync();

        return true;
    }
}