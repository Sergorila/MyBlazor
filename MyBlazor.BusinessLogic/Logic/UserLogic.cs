using Microsoft.Extensions.Logging;
using MyBlazor.BusinessLogic.Interfaces;
using MyBlazor.DataAccess.Entities;
using MyBlazor.DataAccess.Interfaces;

namespace MyBlazor.BusinessLogic.Logic;

public class UserLogic : BaseLogic, IUserLogic
{
    private readonly IUserRepo _repo;
    
    public UserLogic(ILogger<BaseLogic> logger, IUserRepo repo) : base(logger)
    {
        _repo = repo;
    }


    public IEnumerable<User> GetAllAsync()
    {
        try
        {
            Logger.LogInformation("Trying get all users");
            var users = _repo.GetAllAsync();
            Logger.LogInformation("Complete getting users");
            
            return users;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while getting users");
            throw;
        }
    }

    public async Task<User> GetAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying get user: {Id}", id);
            var user = await _repo.GetAsync(id);
            Logger.LogInformation("Complete getting user: {Id}", id);
            
            return user;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while getting user: {Id}", id);
            throw;
        }
    }

    public async Task CreateAsync(User user)
    {
        try
        {
            Logger.LogInformation("Trying add user: {Id}", user.Id);
            await _repo.CreateAsync(user);
            Logger.LogInformation("Complete adding user: {Id}", user.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while adding user: {Id}", user.Id);
            throw;
        }
    }

    public async Task UpdateAsync(User user)
    {
        try
        {
            Logger.LogInformation("Trying update user: {Id}", user.Id);
            await _repo.UpdateAsync(user);
            Logger.LogInformation("Complete updating user: {Id}", user.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while updating user: {Id}", user.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying delete user: {Id}", id);
            var res = await _repo.DeleteAsync(id);
            Logger.LogInformation("Complete deleting user: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while deleting user: {Id}", id);
            throw;
        }
    }

    public async Task<bool> CheckUserAsync(string login, string password)
    {
        try
        {
            Logger.LogInformation("Trying check user: {login}", login);
            var res = await _repo.CheckUserAsync(login, password);
            Logger.LogInformation("Complete checking user: {login}", login);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while checking user: {login}", login);
            throw;
        }
    }
}