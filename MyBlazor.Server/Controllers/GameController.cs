using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlazor.BusinessLogic.Interfaces;
using MyBlazor.DataAccess.Entities;
using MyBlazor.Server.RabbitMQPublisher;
using MyBlazor.Server.Views;

namespace MyBlazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameLogic _gameLogic;
    private readonly IMapper _mapper;
    
    public GameController(IGameLogic gameLogic, IMapper mapper)
    {
        _gameLogic = gameLogic;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("api/getgame")]
    public async Task<IActionResult> GetGame(int id)
    {
        try
        {
            var game = await _gameLogic.GetAsync(id);
            if (game != null)
            {
                var res = _mapper.Map<Game>(game);
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
    [Route("api/getgames")]
    public IActionResult GetGames()
    {
        try
        {
            var games = _gameLogic.GetAllAsync();
            if (games != null)
            {
                return Ok(games);
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
    [Route("api/addgame")]
    public async Task<IActionResult> AddGame(GameView game)
    {
        try
        {
            await _gameLogic.CreateAsync(_mapper.Map<Game>(game));
            GamePublisher.GameCreated(game);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    
    [HttpPut]
    [Route("api/updategame")]
    public async Task<IActionResult> UpdateGame(GameView game)
    {
        try
        {
            await _gameLogic.UpdateAsync(_mapper.Map<Game>(game));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpDelete]
    [Route("api/deletegame")]
    public async Task<IActionResult> RemoveGame(int id)
    {
        if ( await _gameLogic.DeleteAsync(id))
        {
            return Ok();
        }
        else
        {
            return BadRequest("ObjectNotFound");
        }
    }
}