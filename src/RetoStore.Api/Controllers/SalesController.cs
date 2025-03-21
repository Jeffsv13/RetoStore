﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetoStore.Dto.Request;
using RetoStore.Services.Interfaces;
using System.Security.Claims;

namespace RetoStore.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService service;

    public SalesController(ISaleService service)
    {
        this.service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await service.GetAsync(id);
        return response.Success ? Ok(response) : BadRequest(response);
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public async Task<IActionResult> Post(SaleRequestDto request)
    {
        var email = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        var response = await service.AddAsync(email, request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("ListSalesByDate")]
    public async Task<IActionResult> GetByDate([FromQuery] SaleByDateSearchDto? search, [FromQuery] PaginationDto pagination)
    {
        var response = await service.GetAsync(search, pagination);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("ListSales")]
    public async Task<IActionResult> Get(string email, [FromQuery] string? title, [FromQuery] PaginationDto pagination)
    {
        var response = await service.GetAsync(email, title, pagination);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
