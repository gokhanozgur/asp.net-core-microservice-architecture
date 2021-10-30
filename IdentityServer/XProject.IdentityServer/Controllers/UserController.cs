using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using XProject.IdentityServer.Dtos;
using XProject.IdentityServer.Models;
using XProject.Shared.Dtos;
using static IdentityServer4.IdentityServerConstants;

namespace XProject.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)] // This line for local endpoint security (You can check ProjectDevelopmentStep.txt 9.11.2).
    [Route("api/[controller]/[action]")] // Action attribute added for controller method based routing (You can check ProjectDevelopmentStep.txt 9.11.3).
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDto.UserName,
                Email = signUpDto.Email,
                City = signUpDto.City
            };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), (int)HttpStatusCode.BadRequest));
            }

            return NoContent();
        }
    }
}
