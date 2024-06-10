using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlazor.BusinessLogic.Interfaces;
using MyBlazor.DataAccess.Entities;
using MyBlazor.Server.RabbitMQPublisher;
using MyBlazor.Server.Views;

namespace MyBlazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryLogic _categoryLogic;
    private readonly IMapper _mapper;
    
    public CategoryController(ICategoryLogic categoryLogic, IMapper mapper)
    {
        _categoryLogic = categoryLogic;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("api/getcategory")]
    public async Task<IActionResult> GetCategory(int id)
    {
        try
        {
            var category = await _categoryLogic.GetAsync(id);
            if (category != null)
            {
                var res = _mapper.Map<Category>(category);
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
    [Route("api/getcategories")]
    public IActionResult GetCategories()
    {
        try
        {
            var categories = _categoryLogic.GetAllAsync();
            if (categories != null)
            {
                return Ok(categories);
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
    [Route("api/addcategory")]
    public async Task<IActionResult> AddGategory(CategoryView category)
    {
        try
        {
            await _categoryLogic.CreateAsync(_mapper.Map<Category>(category));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    
    [HttpPut]
    [Route("api/updatecategory")]
    public async Task<IActionResult> UpdateCategory(CategoryView category)
    {
        try
        {
            await _categoryLogic.UpdateAsync(_mapper.Map<Category>(category));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpDelete]
    [Route("api/deletecategory")]
    public async Task<IActionResult> RemoveCategory(int id)
    {
        if ( await _categoryLogic.DeleteAsync(id))
        {
            return Ok();
        }
        else
        {
            return BadRequest("ObjectNotFound");
        }
    }
}