using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlazor.BusinessLogic.Interfaces;
using MyBlazor.DataAccess.Entities;
using MyBlazor.Server.RabbitMQPublisher;
using MyBlazor.Server.Views;

namespace MyBlazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderLogic _orderLogic;
    private readonly IMapper _mapper;
    
    public OrderController(IOrderLogic orderLogic, IMapper mapper)
    {
        _orderLogic = orderLogic;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("api/getorder")]
    public async Task<IActionResult> GetOrder(int id)
    {
        try
        {
            var order = await _orderLogic.GetAsync(id);
            if (order != null)
            {
                var res = _mapper.Map<Order>(order);
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
        catch (Exception)
        {
            IActionResult badRequestObjectResult = BadRequest("Bad request.");
            return badRequestObjectResult;
        }
    }
    
    [HttpGet]
    [Route("api/getorders")]
    public IActionResult GetOrders()
    {
        try
        {
            var orders = _orderLogic.GetAllAsync().ToList();
            if (orders != null)
            {
                return Ok(orders);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
        catch (Exception)
        {
            IActionResult badRequestObjectResult = BadRequest("Bad request.");
            return badRequestObjectResult;
        }
    }
    
    [HttpPost]
    [Route("api/addorder")]
    public async Task<IActionResult> AddOrder(OrderView order)
    {
        try
        {
            await _orderLogic.CreateAsync(_mapper.Map<Order>(order));
            
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    
    [HttpPut]
    [Route("api/updateorder")]
    public async Task<IActionResult> UpdateOrder(OrderView order)
    {
        try
        {
            await _orderLogic.UpdateAsync(_mapper.Map<Order>(order));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpDelete]
    [Route("api/deleteorder")]
    public async Task<IActionResult> RemoveOrder(int id)
    {
        if ( await _orderLogic.DeleteAsync(id))
        {
            return Ok();
        }
        else
        {
            return BadRequest("ObjectNotFound");
        }
    }
}