using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZwajApp.API.Data;
using ZwajApp.API.DTOS;

namespace ZwajApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IZwajRepository _zawjRepository;
        private readonly IMapper _imapper;
        public UsersController(IZwajRepository zawjRepository, IMapper imapper)
        {
            _imapper = imapper;
            _zawjRepository = zawjRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _zawjRepository.GetAll();
            var usersToReturn = _imapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _zawjRepository.GetUser(id);
            var userToReturn = _imapper.Map<UserForDetailsDto>(user);
            return Ok(userToReturn);

        }


        [HttpPut("{id}")]
         public async Task<IActionResult> UpdateUser(int id,UserForUpdateDto userForUpdateDto){
             if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
             return Unauthorized();
             var userFromRepo = await _zawjRepository.GetUser(id);
             _imapper.Map(userForUpdateDto,userFromRepo);
             if(await _zawjRepository.SaveAll())
                 return NoContent();
             

             throw new Exception($"حدثت مشكلة في تعديل بيانات المشترك رقم {id}");
             
             
         }
    }
}