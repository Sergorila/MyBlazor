using MyBlazor.DataAccess.Entities;

namespace MyBlazor.DataAccess.Interfaces;

public interface IOrderRepo
{
    IEnumerable<Order> GetAllAsync();
    Task<Order> GetAsync(int id);
    Task CreateAsync(Order item);
    Task UpdateAsync(Order item);
    Task<bool> DeleteAsync(int id);
}