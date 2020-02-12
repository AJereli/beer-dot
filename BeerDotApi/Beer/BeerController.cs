using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeerDotApi.AuthModule;
using BeerDotApi.Beer.DTO;
using BeerDotApi.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BeerDotApi.Beer
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BeerController: ControllerBase
    {
        private readonly ILogger<BeerController> _logger;
        private readonly IBeerService _beerService;
        
        public BeerController(IBeerService beerService, ILogger<BeerController> logger)
        {
            _logger = logger;
            _beerService = beerService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(BeerDto beerDto)
        {
            var userId = Convert.ToInt64(User.Identity.Name);

            if (await _beerService.Add(beerDto, userId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpPost("withReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(BeerWithReviewDto beerDto)
        {
            var userId = Convert.ToInt64(User.Identity.Name);

            if (await _beerService.Add(beerDto, userId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> Get()
        {
            return new JsonResult(await _beerService.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BeerEntity>> Get(long id)
        {
            // JsonResult(id)
            var beer = await _beerService.Get(id);

            if (beer == null)
            {
                return NotFound(id);
            }

            return Ok(beer);
        }
        
        
        
    }
}