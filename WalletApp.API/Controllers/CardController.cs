using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WalletApp.BLL.Services;
using WalletApp.BLL.Services.CardService;
using WalletApp.DAL.ViewModels;

namespace WalletApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : BaseController
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string? id, string? cardNumber)
        {
            id = Request.Query[nameof(id)];
            int number;
            if (int.TryParse(Request.Query[nameof(cardNumber)], out number))
            {
                if (id == null && number == null)
                {
                    var response = await _cardService.GetAllAsync();
                    return GetResult(response);
                }
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var response = await _cardService.GetByIdAsync(id);
                    if (response.Success)
                    {
                        return GetResult(response);
                    }
                }
                if (!int.IsNegative(number))
                {
                    var response = await _cardService.GetByCardNumberAsync(number);
                    if (response.Success)
                    {
                        return GetResult(response);
                    }
                    return GetResult(ServiceResponse.BadRequestResponse("Incorrect card number entered"));
                }
                return GetResult(ServiceResponse.BadRequestResponse("Card fetched"));
            }
            return GetResult(ServiceResponse.BadRequestResponse("Invalid card number entered"));

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] string? id) //лише за айді спеціяльно
        {
            id = Request.Query[nameof(id)];
            if (!string.IsNullOrEmpty(id))
            {
                var response = await _cardService.DeleteAsync(id);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse("Id of the card cannot be empty"));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CardVM model)
        {
            if (!string.IsNullOrWhiteSpace(model.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have an id"));
            }
            if (!string.IsNullOrEmpty(model.CurrencyCode))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have a currency"));
            }
            if (model.CardNumber.ToString().Length > 16 || model.CardNumber.ToString().Length < 16)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid card number"));
            }
            if (model.ExpirationDate.Year < DateTime.UtcNow.Year && model.ExpirationDate.Month < DateTime.UtcNow.Month)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid expiration date"));
            }

            //////////////////////

            var response = await _cardService.CreateAsync(model);
            return GetResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(CardVM model)
        {
            if (!string.IsNullOrWhiteSpace(model.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have an id"));
            }
            if (!string.IsNullOrEmpty(model.CurrencyCode))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have a currency"));
            }
            if (model.CardNumber.ToString().Length > 16 || model.CardNumber.ToString().Length < 16)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid card number"));
            }
            if (model.ExpirationDate.Year < DateTime.UtcNow.Year && model.ExpirationDate.Month < DateTime.UtcNow.Month)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid expiration date"));
            }

            //////////////////

            var response = await _cardService.UpdateAsync(model);
            return GetResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> SetCurrency(CardVM cardModel, CurrencyVM currencyModel)
        {
            if(!string.IsNullOrEmpty(currencyModel.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Currency must have an id"));
            }
            if (!string.IsNullOrEmpty(currencyModel.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Currency must have a name"));
            }
            if (!string.IsNullOrEmpty(currencyModel.Code))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Currency must have a code"));
            }
            if(currencyModel.ExchangeRateUSD == null || currencyModel.ExchangeRateUSD <= 0)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Currency must have an exchange rate above 0"));
            }
            if (!string.IsNullOrWhiteSpace(cardModel.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have an id"));
            }
            if (!string.IsNullOrEmpty(cardModel.CurrencyCode))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have a currency"));
            }
            if (cardModel.CardNumber.ToString().Length > 16 || cardModel.CardNumber.ToString().Length < 16)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid card number"));
            }
            if (cardModel.ExpirationDate.Year < DateTime.UtcNow.Year && cardModel.ExpirationDate.Month < DateTime.UtcNow.Month)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid expiration date"));
            }

            ////////////////////////
            
            var response = await _cardService.SetCurrency(cardModel, currencyModel);
            return GetResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ConvertCurrencies(CardVM cardModel, CurrencyVM currencyModel)
        {
            if (!string.IsNullOrEmpty(currencyModel.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Currency must have an id"));
            }
            if (!string.IsNullOrEmpty(currencyModel.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Currency must have a name"));
            }
            if (!string.IsNullOrEmpty(currencyModel.Code))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Currency must have a code"));
            }
            if (currencyModel.ExchangeRateUSD == null || currencyModel.ExchangeRateUSD <= 0)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Currency must have an exchange rate above 0"));
            }
            if (!string.IsNullOrWhiteSpace(cardModel.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have an id"));
            }
            if (!string.IsNullOrEmpty(cardModel.CurrencyCode))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have a currency"));
            }
            if (cardModel.CardNumber.ToString().Length > 16 || cardModel.CardNumber.ToString().Length < 16)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid card number"));
            }
            if (cardModel.ExpirationDate.Year < DateTime.UtcNow.Year && cardModel.ExpirationDate.Month < DateTime.UtcNow.Month)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid expiration date"));
            }

            //////////////////////////
            
            var response = await _cardService.ConvertCurrencies(cardModel, currencyModel);
            return GetResult(response);
        }
    }
}
