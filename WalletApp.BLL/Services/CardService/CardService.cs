using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.Repositories.CardRepository;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.CardService
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            var cards = await _cardRepository.GetAll();
            if (cards != null)
            {
                var models = _mapper.Map<List<CardVM>>(cards);
                return ServiceResponse.OkResponse("Card received", models);
            }
            return ServiceResponse.BadRequestResponse("Cards not found");
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var card = await _cardRepository.GetByIdAsync(id);
            if (card != null)
            {
                var model = _mapper.Map<CardVM>(card);
                return ServiceResponse.OkResponse("Card received", model);
            }
            return ServiceResponse.BadRequestResponse("Card not found");
        }
        public async Task<ServiceResponse> GetByUsernameAsync(string name)
        {
            var card = await _cardRepository.GetByUsernameAsync(name);
            if (card != null)
            {
                var model = _mapper.Map<CardVM>(card);
                return ServiceResponse.OkResponse("Card received", model);
            }
            return ServiceResponse.BadRequestResponse("Card not found");
        }
        public async Task<ServiceResponse> GetByCardNumberAsync(string cardNumber)
        {
            var card = await _cardRepository.GetByCardNumberAsync(cardNumber);
            if (card != null)
            {
                var model = _mapper.Map<CardVM>(card);
                return ServiceResponse.OkResponse("Card received", model);
            }
            return ServiceResponse.BadRequestResponse("Card not found");
        }
        public async Task<ServiceResponse> CreateAsync(CardVM model)
        {
            var card = _mapper.Map<Card>(model);
            if (card != null)
            {
                await _cardRepository.CreateAsync(card);
                return ServiceResponse.OkResponse("Card created", card);
            }
            return ServiceResponse.BadRequestResponse("Card not created");

        }
        public async Task<ServiceResponse> UpdateAsync(CardVM model)
        {
            var card = _mapper.Map<Card>(model);
            if (card != null)
            {
                await _cardRepository.UpdateAsync(card);
                return ServiceResponse.OkResponse("Card updated", card);
            }
            return ServiceResponse.BadRequestResponse("Card not updated");
        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var card = _cardRepository.GetByIdAsync(id);
            if (card != null)
            {
                await _cardRepository.DeleteAsync(id);
                return ServiceResponse.OkResponse("Card deleted");
            }
            return ServiceResponse.BadRequestResponse("Failed to delete card");
        }
        public async Task<ServiceResponse> SetCurrency(CardVM cardModel, CurrencyVM currencyModel)
        {
            var card = _mapper.Map<Card>(cardModel);
            var currency = _mapper.Map<Currency>(currencyModel);
            if( card != null && currency != null)
            {
                await _cardRepository.SetCurrency(card, currency);
                return ServiceResponse.OkResponse("Currency set", card);
            }
            return ServiceResponse.BadRequestResponse("Currency not set");
            
        }
        public async Task<ServiceResponse> ConvertCurrencies(CardVM cardModel, CurrencyVM convertToCurrencyModel)
        {
            var card = _mapper.Map<Card>(cardModel);
            var currency = _mapper.Map<Currency>(convertToCurrencyModel);
            if (card != null && currency != null)
            {
                await _cardRepository.ConvertCurrencies(card, currency);
                return ServiceResponse.OkResponse("Currency converted", card);
            }
            return ServiceResponse.BadRequestResponse("Currency not converted");
        }
    }
}
