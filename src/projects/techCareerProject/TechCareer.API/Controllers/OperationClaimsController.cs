using Microsoft.AspNetCore.Mvc;
using TechCareer.Models.Dtos.Roles;
using TechCareer.Service.Abstracts;

namespace TechCareer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationClaimsController(IOperationClaimService operationClaimService) : Controller
{

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await operationClaimService.GetAllAsync(cancellationToken);
        return Ok(result);
    }



    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] OperationClaimAddRequestDto dto,CancellationToken cancellationToken)
    {
        var result = await operationClaimService.AddAsync(dto,cancellationToken);
        return Ok(result);
    }
    
    
    
}