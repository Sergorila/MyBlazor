using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlazor.BusinessLogic.Interfaces;
using MyBlazor.DataAccess.Entities;
using MyBlazor.Server.RabbitMQPublisher;
using MyBlazor.Server.Views;

namespace MyBlazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IMapper _mapper;
    
    public UserController(IUserLogic userlogic, IMapper mapper)
    {
        _userLogic = userlogic;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("/api/getuser")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var user = await _userLogic.GetAsync(id);
            if (user != null)
            {
                var res = _mapper.Map<User>(user);
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

    [HttpPost]
    [Route("/api/adduser")]
    public async Task<IActionResult> AddUser(UserView user)
    {
        try
        {
            await _userLogic.CreateAsync(_mapper.Map<User>(user));
            UserPublisher.UserCreated(user);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }

    [HttpPost]
    [Route("/api/checkuser")]
    public async Task<IActionResult> CheckUser(string login, string password)
    {
        try
        {
            var res =  await _userLogic.CheckUserAsync(login, password);
            if (res)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }

    [HttpPut]
    [Route("/api/updateuser")]
    public async Task<IActionResult> UpdateUser(UserView user)
    {
        try
        {
            await _userLogic.UpdateAsync(_mapper.Map<User>(user));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }

    [HttpDelete]
    [Route("/api/deleteuser")]
    public async Task<IActionResult> RemoveUser(int id)
    {
        if ( await _userLogic.DeleteAsync(id))
        {
            return Ok();
        }
        else
        {
            return BadRequest("ObjectNotFound");
        }
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> LoginUser(string login, string password)
    {
        if (await _userLogic.CheckUserAsync(login, password))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, login.ToLower()),
            };
            
            ClaimsIdentity claimsIdentity = new(
                claims, 
                "Token", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: "MyAuthServer",
                audience: "MyAuthClient",
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(1)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("AMOGU$AB1GU$SUg0M4")),
                    SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                user_name = claimsIdentity.Name,
            };
            
            HttpContext.Response.Cookies.Append("access_token", encodedJwt);
            
            return Ok(response);
        }
        
        return Unauthorized();
    }
}