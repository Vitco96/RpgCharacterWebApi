using Microsoft.AspNetCore.Mvc;
using Tutorial_proj.Models;
using Tutorial_proj.Services.CharacterService;

namespace Tutorial_proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        #region Fields & Properties 

        private readonly ICharacterSevice _characterService;

        #endregion

        #region Constructor

        public CharacterController(ICharacterSevice characterSevice)
        {
            _characterService = characterSevice ?? throw new ArgumentNullException(nameof(characterSevice));
        }

        #endregion

        #region Methods

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            return Ok(await _characterService.UpdateCharacter(updatedCharacter));
        }

        #endregion
    }
}
