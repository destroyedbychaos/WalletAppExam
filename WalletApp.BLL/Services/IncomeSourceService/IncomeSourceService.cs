using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.Repositories.IncomeSourceRepository;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.IncomeSourceService
{
    public class IncomeSourceService : IIncomeSourceService
    {
        private readonly IIncomeSourceRepository _incomeSourceRepository;
        private readonly IMapper _mapper;
        public IncomeSourceService(IIncomeSourceRepository incomeSourceRepository, IMapper mapper) 
        { 
            _incomeSourceRepository = incomeSourceRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            var incomeSources = await _incomeSourceRepository.GetAll();
            return ServiceResponse.OkResponse("Received income sources", incomeSources);
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var incomeSource = _incomeSourceRepository.GetByIdAsync(id);
            if (incomeSource != null)
            {
                return ServiceResponse.OkResponse("Received income source", incomeSource);
            }
            return ServiceResponse.BadRequestResponse("Income source not found");
        }
        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            var incomeSource = _incomeSourceRepository.GetByNameAsync(name);
            if (incomeSource != null)
            {
                return ServiceResponse.OkResponse("Received income source", incomeSource);
            }
            return ServiceResponse.BadRequestResponse("Income source not found");
        }
        public async Task<ServiceResponse> CreateAsync(IncomeSourceVM incomeSourceModel)
        {
            var incomeSource = _mapper.Map<IncomeSource>(incomeSourceModel);
            if (incomeSource != null)
            {
                await _incomeSourceRepository.CreateAsync(incomeSource);
                return ServiceResponse.OkResponse("Created income source", incomeSource);
            }
            return ServiceResponse.BadRequestResponse("Income source not created");
        }
        public async Task<ServiceResponse> UpdateAsync(IncomeSourceVM incomeSourceModel)
        {
            var incomeSource = _mapper.Map<IncomeSource>(incomeSourceModel);
            if (incomeSource != null)
            {
                await _incomeSourceRepository.UpdateAsync(incomeSource);
                return ServiceResponse.OkResponse("Updated income source", incomeSource);
            }
            return ServiceResponse.BadRequestResponse("Income source not updated");
        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var incomeSource = await _incomeSourceRepository.GetByIdAsync(id);
            if (incomeSource != null)
            {
                await _incomeSourceRepository.CreateAsync(incomeSource);
                return ServiceResponse.OkResponse("Created income source", incomeSource);
            }
            return ServiceResponse.BadRequestResponse("Income source not created");
        }
    }
}
