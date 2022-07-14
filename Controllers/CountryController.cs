using AutoMapper;
using HotelListing.IRepository;
using HotelListing.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CountryController> logger;
        private readonly IMapper mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await unitOfWork.Countries.GetAll();
                var result = mapper.Map<IList<CountryDTO>>(countries);

                return !result.Any() ? NotFound() : Ok(result);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wroung in the {nameof(CountryController)}");
                return StatusCode(500, "Internal Server Error. Please try again.");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await unitOfWork.Countries.Get(c=> c.Id == id, new List<string> {"Hotels"});
                var result = mapper.Map<CountryDTO>(country);

                return result == null ? NotFound() : Ok(result);
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wroung in the {nameof(CountryController)}");
                return StatusCode(500, "Internal Server Error. Please try again.");
            }
        }
    }
}
