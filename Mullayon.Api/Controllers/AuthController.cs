using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mullayon.Api.Dtos;
using Mullayon.Api.Extensions;
using Mullayon.Core;
using Mullayon.Core.Constants;
using Mullayon.Core.Entities;

namespace Mullayon.Api.Controllers;

public class AuthController : BaseController
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthController(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper) : base(unitOfWork,
        mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password)) return Unauthorized();
        var userRoles = await _userManager.GetRolesAsync(user);
        
        Console.WriteLine("***********************");
        Console.WriteLine(userRoles.Count);
        Console.WriteLine("***********************");

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id),
        };
        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var token = GetToken(authClaims);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = _mapper.Map<ApplicationUser>(dto);

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, RoleStrings.User);
            return Ok();
        }

        return BadRequest();
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            // audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetUser()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var userRoles = await _userManager.GetRolesAsync(user);
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Roles = userRoles;
        return Ok(userDto);
    }

    [HttpGet("users")]
    [Authorize(Roles = RoleStrings.Admin)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return Ok(usersDto);
    }

    [HttpPut("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto dto)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user.Id != dto.Id && user.Id != User.GetId().ToString())
        {
            return Forbid();
        }

        Mapper.Map(dto, user);
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return NoContent();
        }

        return BadRequest();
    }

    [HttpPut("user/{id:guid}/password")]
    [Authorize]
    public async Task<IActionResult> UpdateUserPassword(Guid id, [FromBody] UpdatePasswordDto dto)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user.Id != id.ToString() && user.Id != User.GetId().ToString())
        {
            return Forbid();
        }

        var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
        if (result.Succeeded)
        {
            return NoContent();
        }

        return BadRequest();
    }
}