using MyBlazor.DataAccess.Entities;

namespace MyBlazor.BusinessLogic.Interfaces;

public interface IGameLogic
{
    IEnumerable<Game> GetAllAsync();
    Task<Game> GetAsync(int id);
    Task CreateAsync(Game item);
    Task UpdateAsync(Game item);
    Task<bool> DeleteAsync(int id);
}