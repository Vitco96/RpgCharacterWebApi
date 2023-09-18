using System.ComponentModel;
using AutoMapper;
using Tutorial_proj.Models;

namespace Tutorial_proj.Services.CharacterService
{
    public class CharacterService : ICharacterSevice
    {
        #region Fields and Properties

        public List<Character> characters = new List<Character>{
            new Character(),
            new Character{ Id =1, Name = "Sam"}
        };
        
        #endregion

        #region Constructor and Dependencies

        private readonly IMapper _mapper;
        
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
            var character = characters.FirstOrDefault(c => c.Id == updatedharacter.Id);

            if(character is null)
            {
                throw new Exception($"Character with Id '{updatedharacter.Id}' not found");
            }

            character.Name = updatedharacter.Name;
            character.Hitpoints = updatedharacter.Hitpoints;
            character.Strenght = updatedharacter.Strenght;
            character.Defense = updatedharacter.Defense;
            character.Intelligence = updatedharacter.Intelligence;
            character.Class = updatedharacter.Class;

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            } 
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
              var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
            var character = characters.First(c => c.Id == id);

            if(character is null)
            {
                throw new Exception($"Character with Id '{id}' not found");
            }

            characters.Remove(character);

            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            } 
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        #endregion
    }
}