using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetoStore.Dto.Request;
using RetoStore.Repositories.Interfaces;
using RetoStore.Services.Interfaces;

namespace RetoStore.Api.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService service;

        public EventsController(
            IEventService service)
        {
            this.service = service;
        }

        [HttpGet("title")]
        [AllowAnonymous]
        public async Task<ActionResult> Get(string? title, [FromQuery] PaginationDto pagination)
        {
            var response = await service.GetAsync(title, pagination);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] EventRequestDto request)
        {
            var response = await service.AddAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] EventRequestDto request)
        {
            var response = await service.UpdateAsync(id, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return Ok(response);
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id)
        {
            return Ok(await service.FinalizeAsync(id));
        }
    }
}
