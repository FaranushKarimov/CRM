using contracts.Services;
using entities.DataTransferObjects.JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/api/login")]
        public async Task<IActionResult> Authentication(AuthenticationRequest request)
        {
            return Ok( await _userService.AuthenticateAsync(request));
        }

        [HttpGet("/api/updateStatus")]
        public async Task<IActionResult> UpdateStatus(int id, int compilanceStatusId, string objectType, string note)
        {
            await _userService.UpdateStatusComplience(id, compilanceStatusId, objectType, note);
            return Ok();
        }
    }
}
