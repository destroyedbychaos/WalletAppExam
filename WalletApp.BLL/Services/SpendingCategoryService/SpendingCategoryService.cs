using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.Repositories.SpendingCategoryRepository;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.SpendingCategoryService
{
    public class SpendingCategoryService : ISpendingCategoryService
    {
        private readonly ISpendingCategoryRepository _spendingCategoryRepository;
        private readonly IMapper _mapper;
        public SpendingCategoryService(ISpendingCategoryRepository spendingCategoryRepository, IMapper mapper)
        {
            _spendingCategoryRepository = spendingCategoryRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            var spendingCategories = _spendingCategoryRepository.GetAll();
            return ServiceResponse.OkResponse("Spending categories retrieved");
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var spendingCategory = _spendingCategoryRepository.GetByIdAsync(id);
            if (spendingCategory != null)
            {
                return ServiceResponse.OkResponse("Spending category retrieved", spendingCategory);
            }
            return ServiceResponse.BadRequestResponse("Spending category not retrieved");
        }
        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            var spendingCategory = _spendingCategoryRepository.GetByNameAsync(name);
            if (spendingCategory != null)
            {
                return ServiceResponse.OkResponse("Spending category retrieved", spendingCategory);
            }
            return ServiceResponse.BadRequestResponse("Spending category not retrieved");
        }
        public async Task<ServiceResponse> CreateAsync(SpendingCategoryVM model)
        {
            var spendingCategory = _mapper.Map<SpendingCategory>(model);
            if(spendingCategory != null)
            {
                await _spendingCategoryRepository.CreateAsync(spendingCategory);
                return ServiceResponse.OkResponse("Spending category created");
            }
            return ServiceResponse.BadRequestResponse("Spending category not created");
        }
        public async Task<ServiceResponse> UpdateAsync(SpendingCategoryVM model)
        {
            var spendingCategory = _mapper.Map<SpendingCategory>(model);
            if (spendingCategory != null)
            {
                await _spendingCategoryRepository.UpdateAsync(spendingCategory);
                return ServiceResponse.OkResponse("Spending category updated");
            }
            return ServiceResponse.BadRequestResponse("Spending category not updated");
        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var spendingCategory = await _spendingCategoryRepository.GetByIdAsync(id);
            if (spendingCategory != null)
            {
                await _spendingCategoryRepository.DeleteAsync(id);
                return ServiceResponse.OkResponse("Spending category deleted");
            }
            return ServiceResponse.BadRequestResponse("Spending category not deleted");
        }
    }
}
