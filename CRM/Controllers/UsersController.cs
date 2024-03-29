﻿using contracts.Services;
using entities.DataTransferObjects.ArchiveDTO;
using entities.DataTransferObjects.JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [HttpPut("/api/updateStatus")]
        public async Task<IActionResult> UpdateStatus(int id, int compilanceStatusId, string ObjectType,string note)
        {
            await _userService.PutRequestComplience(id, compilanceStatusId, ObjectType, note);
            return Ok();
        }

        [HttpGet("/api/getUserByCode")]
        public IActionResult GetUserByCode(string route)
        {
            return Ok(_userService.GetUserByCode(route));
        }

        [HttpGet("/api/getAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("/api/getAllUsersExcel")]
        public async Task<IActionResult> GetAllUsersExcel()
        {
            var users = await _userService.GetAllUsersAsExcel();
            return File(users.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Пользователи.xlsx");
        }
        [HttpPost("/api/Search")]
        public async Task<IActionResult> Search(string search, string searchType, [Range(2,3)]int? complianceStatus, [FromQuery]List<string> dates)
        {
            return Ok(await _userService.Search(search, searchType, complianceStatus,dates));
        }

        [HttpPost("/api/GetAllUsersArchive")]
        public IActionResult GetAllUsersArchive([FromBody] GetAllWithFilterDTO dto)
        {
            return Ok(_userService.GetUserListArchive(dto));
        }
    }
}
