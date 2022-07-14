using AutoMapper;
using HotelListing.Data;
using HotelListing.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly SignInManager<ApiUser> signInManager;

        public AccountController(IMapper mapper,UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Result ([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = mapper.Map<ApiUser>(userDTO);
                var result = await userManager.CreateAsync(user, userDTO.Password);
                if (!result.Succeeded)
                {
                    return BadRequest("");

                }

                return Accepted();

            }catch(Exception ex)
            {
                return Problem($"Something went wroung in the {nameof(AccountController)}", statusCode: 500);
            }
        }
    }
}
