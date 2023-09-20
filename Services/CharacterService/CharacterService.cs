using System.ComponentModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tutorial_proj.Data;
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
        private readonly DataContext _context;
        
        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region GET Methods 

         public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
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