using AutoMapper;
using Core.Persistence.Extensions;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCareer.Models.Dtos.Users;
using TechCareer.Service.Abstracts;

namespace TechCareer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetAsync(u => u.Id == id, cancellationToken: cancellationToken);
        if (user == null)
            return NotFound("User not found.");

        var response = _mapper.Map<UserResponseDto>(user);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetListAsync(cancellationToken: cancellationToken);
        var response = _mapper.Map<List<UserResponseDto>>(users);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add(User user)
    {
        var addedUser = await _userService.AddAsync(user);
        return Ok(addedUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var updatedUser = await _userService.UpdateAsync(user);
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, bool permanent = false)
    {
        var existingUser = await _userService.GetAsync(u => u.Id == id);
        if (existingUser == null)
            return NotFound("User not found.");

        await _userService.DeleteAsync(existingUser, permanent);
        return NoContent();
    }
    [HttpGet("paginate")]
    public async Task<IActionResult> GetPaginate(
    [FromQuery] int index = 0,
    [FromQuery] int size = 10,
    CancellationToken cancellationToken = default)
    {
        var paginatedUsers = await _userService.GetPaginateAsync(
            index: index,
            size: size,
            cancellationToken: cancellationToken
        );

        // Sonuçları DTO'ya map etmek isteyebilirsiniz
        var response = new Paginate<UserResponseDto>
        {
            Items = _mapper.Map<IList<UserResponseDto>>(paginatedUsers.Items),
            Index = paginatedUsers.Index,
            Size = paginatedUsers.Size,
            Count = paginatedUsers.Count,
            Pages = paginatedUsers.Pages
        };

        return Ok(response);
    }
}
