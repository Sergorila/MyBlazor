using Microsoft.Extensions.Logging;
using MyBlazor.BusinessLogic.Interfaces;
using MyBlazor.DataAccess.Entities;
using MyBlazor.DataAccess.Interfaces;

namespace MyBlazor.BusinessLogic.Logic;

public class GameLogic : BaseLogic, IGameLogic
{
    private readonly IGameRepo _repo;
    
    public GameLogic(ILogger<BaseLogic> logger, IGameRepo repo) : base(logger)
    {
        _repo = repo;
    }


    public IEnumerable<Game> GetAllAsync()
    {
        try
        {
            Logger.LogInformation("Trying get all games");
            var games = _repo.GetAllAsync();
            Logger.LogInformation("Complete getting games");
            
            return games;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while getting games");
            throw;
        }
    }

    public async Task<Game> GetAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying get game: {Id}", id);
            var game = await _repo.GetAsync(id);
            Logger.LogInformation("Complete getting game: {Id}", id);
            
            return game;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while getting game: {Id}", id);
            throw;
        }
    }

    public async Task CreateAsync(Game game)
    {
        try
        {
            Logger.LogInformation("Trying add game: {Id}", game.Id);
            await _repo.CreateAsync(game);
            Logger.LogInformation("Complete adding game: {Id}", game.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while adding game: {Id}", game.Id);
            throw;
        }
    }

    public async Task UpdateAsync(Game game)
    {
        try
        {
            Logger.LogInformation("Trying update category: {Id}", game.Id);
            await _repo.UpdateAsync(game);
            Logger.LogInformation("Complete updating category: {Id}", game.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while updating category: {Id}", game.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying delete game: {Id}", id);
            var res = await _repo.DeleteAsync(id);
            Logger.LogInformation("Complete deleting game: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while deleting game: {Id}", id);
            throw;
        }
    }
}