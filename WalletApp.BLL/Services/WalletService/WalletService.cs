using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.Repositories.CardRepository;
using WalletApp.DAL.Repositories.WalletRepository;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.WalletService
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;
        public WalletService(IWalletRepository walletRepository, IMapper mapper, ICardRepository cardRepository)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
            _cardRepository = cardRepository;
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
        public async Task<ServiceResponse> GetByUsernameAsync(string name)
        {
            var wallet = _walletRepository.GetByUsernameAsync(name);
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
                return ServiceResponse.OkResponse("Card added to wallet");
            }
            return ServiceResponse.BadRequestResponse("Card not added to wallet");

        }
    }
}
