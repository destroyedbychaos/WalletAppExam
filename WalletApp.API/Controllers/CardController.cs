using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WalletApp.BLL.Services;
using WalletApp.BLL.Services.CardService;
using WalletApp.BLL.Validators;
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
        [HttpDelete("DeleteCard")]
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
        [HttpPost("CreateCard")]
        public async Task<IActionResult> CreateAsync(CardVM model)
        {
            var validator = new CardValidator();
            var validateResult = await validator.ValidateAsync(model);

            if (validateResult.IsValid)
            {
                var response = await _cardService.CreateAsync(model);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse("Invalid card info entered"));
        }
        [HttpPost("UpdateCard")]
        public async Task<IActionResult> UpdateAsync(CardVM model)
        {
            var validator = new CardValidator();
            var validateResult = await validator.ValidateAsync(model);

            if (validateResult.IsValid)
            {
                var response = await _cardService.UpdateAsync(model);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse("Invalid card info entered"));
        }

        [HttpPost("SetCurrency")]
        public async Task<IActionResult> SetCurrency(CardCurrencyVM model)
        {
            var validatorCard = new CardValidator();
            var validatorCurrency = new CurrencyValidator();
            var validateResultCard = await validatorCard.ValidateAsync(model.Card);
            var validateResultCurrency = await validatorCurrency.ValidateAsync(model.Currency);

            if(validateResultCard.IsValid && validateResultCurrency.IsValid)
            {
                var response = await _cardService.SetCurrency(model.Card, model.Currency);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse("Invalid info entered"));
        }

        [HttpPost("ConvertCardToCurrency")]
        public async Task<IActionResult> ConvertCurrencies(CardCurrencyVM model)
        {
            var validatorCard = new CardValidator();
            var validatorCurrency = new CurrencyValidator();
            var validateResultCard = await validatorCard.ValidateAsync(model.Card);
            var validateResultCurrency = await validatorCurrency.ValidateAsync(model.Currency);

            if (validateResultCard.IsValid && validateResultCurrency.IsValid)
            {
                var response = await _cardService.ConvertCurrencies(model.Card, model.Currency);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse("Invalid info entered"));


        }
    }
}
