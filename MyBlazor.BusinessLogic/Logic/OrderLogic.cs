using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyBlazor.BusinessLogic.Interfaces;
using MyBlazor.DataAccess.Entities;
using MyBlazor.DataAccess.Interfaces;
using MyBlazor.DTO;
using MyBlazor.Server.RabbitMQPublisher;

namespace MyBlazor.BusinessLogic.Logic;

public class OrderLogic : BaseLogic, IOrderLogic
{
    private readonly IOrderRepo _repo;
    private readonly OrderPublisher _orderPublisher;
    
    public OrderLogic(ILogger<BaseLogic> logger, IOrderRepo repo, OrderPublisher orderPublisher) : base(logger)
    {
        _repo = repo;
        _orderPublisher = orderPublisher;
    }


    public IEnumerable<Order> GetAllAsync()
    {
        try
        {
            Logger.LogInformation("Trying get all orders");
            var orders = _repo.GetAllAsync();
            Logger.LogInformation("Complete getting orders");
            
            return orders;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while getting orders");
            throw;
        }
    }

    public async Task<Order> GetAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying get order: {Id}", id);
            var dto = new OrderGetDTO() { Id = id };
            var serializedDto = JsonSerializer.Serialize(dto);
            var order = (string)await _orderPublisher.SendAsync(serializedDto, default);
            Logger.LogInformation("Complete getting order: {Id}", id);
            
            return JsonSerializer.Deserialize<Order>(order);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while getting order: {Id}", id);
            throw;
        }
    }

    public async Task CreateAsync(Order order)
    {
        try
        {
            Logger.LogInformation("Trying add order: {Id}", order.Id);
            await _repo.CreateAsync(order);
            Logger.LogInformation("Complete adding order: {Id}", order.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while adding order: {Id}", order.Id);
            throw;
        }
    }

    public async Task UpdateAsync(Order order)
    {
        try
        {
            Logger.LogInformation("Trying update category: {Id}", order.Id);
            await _repo.UpdateAsync(order);
            Logger.LogInformation("Complete updating category: {Id}", order.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while updating category: {Id}", order.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying delete order: {Id}", id);
            var res = await _repo.DeleteAsync(id);
            Logger.LogInformation("Complete deleting order: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while deleting order: {Id}", id);
            throw;
        }
    }
}