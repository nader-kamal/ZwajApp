using System.Collections.Generic;
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
            var usersToReturn=_imapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _zawjRepository.GetUser(id);
            var userToReturn=_imapper.Map<UserForDetailsDto>(user);
            return Ok(userToReturn);

        }
    }
}