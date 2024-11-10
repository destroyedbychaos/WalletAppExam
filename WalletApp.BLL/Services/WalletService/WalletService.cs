using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.Repositories.CardRepository;
using WalletApp.DAL.Repositories.IncomeSourceRepository;
using WalletApp.DAL.Repositories.SpendingCategoryRepository;
using WalletApp.DAL.Repositories.WalletRepository;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.WalletService
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;
        private readonly IIncomeSourceRepository _incomeSourceRepository;
        private readonly ISpendingCategoryRepository _spendingCategoryRepository;
        public WalletService(IWalletRepository walletRepository, IMapper mapper, ICardRepository cardRepository, IIncomeSourceRepository incomeSourceRepository, ISpendingCategoryRepository spendingCategoryRepository)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
            _cardRepository = cardRepository;
            _incomeSourceRepository = incomeSourceRepository;
            _spendingCategoryRepository = spendingCategoryRepository;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            var wallets = _walletRepository.GetAllAsync();
            return ServiceResponse.OkResponse("Retrieved all wallets", wallets);
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var wallet = _walletRepository.GetByIdAsync(id);
            if (wallet != null)
            {
                return ServiceResponse.OkResponse("Retrieved wallet", wallet);
            }
            return ServiceResponse.BadRequestResponse("Wallet not retrieved");
        }
        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            var wallet = _walletRepository.GetByNameAsync(name);
            if (wallet != null)
            {
                return ServiceResponse.OkResponse("Retrieved wallet", wallet);
            }
            return ServiceResponse.BadRequestResponse("Wallet not retrieved");
        }
        public async Task<ServiceResponse> CreateAsync(WalletVM model)
        {
            var wallet = _mapper.Map<Wallet>(model);
            if (wallet != null)
            {
                await _walletRepository.CreateAsync(wallet);
                return ServiceResponse.OkResponse("Wallet created", wallet);
            }
            return ServiceResponse.BadRequestResponse("Wallet not created");
        }
        public async Task<ServiceResponse> UpdateAsync(WalletVM model)
        {
            var wallet = _mapper.Map<Wallet>(model);
            if (wallet != null)
            {
                await _walletRepository.UpdateAsync(wallet);
                return ServiceResponse.OkResponse("Wallet updated", wallet);
            }
            return ServiceResponse.BadRequestResponse("Wallet not updated");
        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var wallet = _walletRepository.GetByIdAsync(id);
            if (wallet != null)
            {
                await _walletRepository.DeleteAsync(id);
                return ServiceResponse.OkResponse("Wallet deleted");
            }
            return ServiceResponse.BadRequestResponse("Wallet not deleted");
        }
        public async Task<ServiceResponse> AddCardToWalletAsync(WalletVM walletModel, CardVM cardModel)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletModel.Id);
            var card = await _cardRepository.GetByIdAsync(cardModel.Id);
            if (wallet != null && card != null) 
            {
                wallet.Cards.Add(card);
                await _walletRepository.UpdateAsync(wallet);
                return ServiceResponse.OkResponse("Card added to wallet", wallet);
            }
            return ServiceResponse.BadRequestResponse("Card not added to wallet");
        }
        public async Task<ServiceResponse> DeleteCardFromWalletAsync(WalletVM walletModel, CardVM cardModel)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletModel.Id);
            var card = await _cardRepository.GetByIdAsync(cardModel.Id);
            if (wallet != null && card != null)
            {
                wallet.Cards.Add(card);
                await _walletRepository.DeleteCardFromWalletAsync(wallet, card);
                return ServiceResponse.OkResponse("Card deleted from wallet", wallet);
            }
            return ServiceResponse.BadRequestResponse("Card not deleted from wallet");
        }
        public async Task<ServiceResponse> AddIncomeSourceToWalletAsync(WalletVM walletModel, IncomeSourceVM incomeSourceModel)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletModel.Id);
            var incomeSource = await _incomeSourceRepository.GetByIdAsync(incomeSourceModel.Id);
            if(wallet != null && incomeSource != null)
            {
                await _walletRepository.AddIncomeSourceToWalletAsync(wallet, incomeSource);
                return ServiceResponse.OkResponse("Added income source to wallet", wallet);
            }
            return ServiceResponse.BadRequestResponse("Failed to add income source to wallet");
        }
        public async Task<ServiceResponse> DeleteIncomeSourceFromWalletAsync(WalletVM walletModel, IncomeSourceVM incomeSourceModel)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletModel.Id);
            var incomeSource = await _incomeSourceRepository.GetByIdAsync(incomeSourceModel.Id);
            if (wallet != null && incomeSource != null)
            {
                await _walletRepository.DeleteIncomeSourceFromWalletAsync(wallet, incomeSource);
                return ServiceResponse.OkResponse("Deleted income source from wallet", wallet);
            }
            return ServiceResponse.BadRequestResponse("Failed to delete income source from wallet");
        }
        public async Task<ServiceResponse> AddSpendingCategoryToWalletAsync(WalletVM walletModel, SpendingCategoryVM spendingCategoryModel)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletModel.Id);
            var spendingCategory = await _spendingCategoryRepository.GetByIdAsync(spendingCategoryModel.Id);
            if (wallet != null && spendingCategory != null)
            {
                await _walletRepository.AddSpendingCategoryToWalletAsync(wallet, spendingCategory);
                return ServiceResponse.OkResponse("Added spending category to wallet", wallet);
            }
            return ServiceResponse.BadRequestResponse("Failed to add spending category to wallet");
        }
        public async Task<ServiceResponse> DeleteSpendingCategoryFromWalletAsync(WalletVM walletModel, SpendingCategoryVM spendingCategoryModel)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletModel.Id);
            var spendingCategory = await _spendingCategoryRepository.GetByIdAsync(spendingCategoryModel.Id);
            if (wallet != null && spendingCategory != null)
            {
                await _walletRepository.DeleteSpendingCategoryFromWalletAsync(wallet, spendingCategory);
                return ServiceResponse.OkResponse("Deleted spending category from wallet", wallet);
            }
            return ServiceResponse.BadRequestResponse("Failed to delete spending category from wallet");
        }
    }
}
