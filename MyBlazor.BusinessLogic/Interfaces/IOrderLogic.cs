using MyBlazor.DataAccess.Entities;

namespace MyBlazor.BusinessLogic.Interfaces;

public interface IOrderLogic
{
    IEnumerable<Order> GetAllAsync();
    Task<Order> GetAsync(int id);
    Task CreateAsync(Order item);
    Task UpdateAsync(Order item);
    Task<bool> DeleteAsync(int id);
}