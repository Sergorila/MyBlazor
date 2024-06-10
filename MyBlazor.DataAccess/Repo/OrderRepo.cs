using MyBlazor.DataAccess.Context;
using MyBlazor.DataAccess.Entities;
using MyBlazor.DataAccess.Interfaces;

namespace MyBlazor.DataAccess.Repo;

public class OrderRepo : BaseRepo, IOrderRepo
{
    public OrderRepo(MyContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<Order> GetAllAsync()
    {
        return myContext.Orders;
    }

    public async Task<Order> GetAsync(int id)
    {
        var order = await myContext.Orders.FindAsync(id);
        return order;
    }

    public async Task CreateAsync(Order order)
    {
        await myContext.Orders.AddAsync(order);
        await myContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        myContext.Orders.Update(order);
        await myContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await myContext.Orders.FindAsync(id);

        myContext.Orders.Remove(order);

        await myContext.SaveChangesAsync();

        return true;
    }
}