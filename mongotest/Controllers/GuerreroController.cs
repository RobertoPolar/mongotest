using Microsoft.AspNetCore.Mvc;
using mongotest.Models;
using mongotest.Services;

namespace mongotest.Controllers
{
    [ApiController]
    [Route("api/")]
    public class GuerreroController : ControllerBase
    {
        private readonly GuerreroService _service;
        public GuerreroController(GuerreroService service)
        {
            _service = service;
        }

        [HttpGet("knights")]
        public IActionResult Get()
        {
            return Ok(_service.Get());
        }

        [HttpGet("/knights")]
        public IActionResult GetHeroes([FromQuery] string filter)
        {
            return Ok(_service.Get());
        }

        [HttpPost("knights")]
        public IActionResult Create(Guerrero guerrero)
        {
            _service.Create(guerrero);

            return Ok();
        }

        [HttpGet("knights/{id}")]
        public IActionResult GetHeroeById([FromRoute] string id)
        {
            return Ok(_service.GetByID(id));
        }


        [HttpDelete("knights/{id}")]
        public IActionResult DeleteHeroeById([FromRoute] string id)
        {
            _service.Delete(id);

            return Ok();
        }

        [HttpPut("knights/{id}")]
        public IActionResult UpdateHeroe(Guerrero guerrero)
        {
            _service.Update(guerrero);

            return Ok();
        }
    }
}
