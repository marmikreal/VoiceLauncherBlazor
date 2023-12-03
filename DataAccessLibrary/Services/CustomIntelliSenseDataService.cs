using Ardalis.GuardClauses;
using AutoMapper;
using DataAccessLibrary.DTO;
using DataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VoiceLauncher.Services
{
   public class CustomIntelliSenseDataService : ICustomIntelliSenseDataService
    {
        private readonly ICustomIntelliSenseRepository _customIntelliSenseRepository;

        public CustomIntelliSenseDataService(ICustomIntelliSenseRepository customIntelliSenseRepository)
        {
            _customIntelliSenseRepository = customIntelliSenseRepository;
        }
        public async Task<List<CustomIntelliSenseDTO>> GetAllCustomIntelliSensesAsync(int LanguageId, int CategoryId, int pageNumber, int pageSize)
        {
            var CustomIntelliSenses = await _customIntelliSenseRepository.GetAllCustomIntelliSensesAsync(LanguageId,CategoryId,pageNumber,pageSize);
            return CustomIntelliSenses.ToList();
        }
        public async Task<List<CustomIntelliSenseDTO>> SearchCustomIntelliSensesAsync(string serverSearchTerm)
        {
            var CustomIntelliSenses = await _customIntelliSenseRepository.SearchCustomIntelliSensesAsync(serverSearchTerm);
            return CustomIntelliSenses.ToList();
        }

        public async Task<CustomIntelliSenseDTO> GetCustomIntelliSenseById(int Id)
        {
            var customIntelliSense = await _customIntelliSenseRepository.GetCustomIntelliSenseByIdAsync(Id);
            return customIntelliSense;
        }
        public async Task<CustomIntelliSenseDTO> AddCustomIntelliSense(CustomIntelliSenseDTO customIntelliSenseDTO)
        {
            Guard.Against.Null(customIntelliSenseDTO);
            var result = await _customIntelliSenseRepository.AddCustomIntelliSenseAsync(customIntelliSenseDTO);
            if (result == null)
            {
                throw new Exception($"Add of customIntelliSense failed ID: {customIntelliSenseDTO.Id}");
            }
            return result;
        }
        public async Task<CustomIntelliSenseDTO> UpdateCustomIntelliSense(CustomIntelliSenseDTO customIntelliSenseDTO, string username)
        {
            Guard.Against.Null(customIntelliSenseDTO);
            Guard.Against.Null(username);
            var result = await _customIntelliSenseRepository.UpdateCustomIntelliSenseAsync(customIntelliSenseDTO);
            if (result == null)
            {
                throw new Exception($"Update of customIntelliSense failed ID: {customIntelliSenseDTO.Id}");
            }
            return result;
        }

        public async Task DeleteCustomIntelliSense(int Id)
        {
            await _customIntelliSenseRepository.DeleteCustomIntelliSenseAsync(Id);
        }
    }
}