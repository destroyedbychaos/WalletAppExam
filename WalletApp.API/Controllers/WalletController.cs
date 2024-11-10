using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WalletApp.BLL.Services;
using WalletApp.BLL.Services.WalletService;
using WalletApp.DAL.ViewModels;

namespace WalletApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : BaseController
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string? id, string? name)
        {
            id = Request.Query[nameof(id)];
            name = Request.Query[nameof(name)];

            if (id == null && name == null)
            {
                var response = await _walletService.GetAllAsync();
                return GetResult(response);
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                var response = await _walletService.GetByIdAsync(id);
                if (response.Success)
                {
                    return GetResult(response);
                }
            }
            if(!string.IsNullOrWhiteSpace(name))
            {
                var response = await _walletService.GetByNameAsync(name);
                if(response.Success)
                {
                    return GetResult(response);
                }
            }
            return GetResult(ServiceResponse.BadRequestResponse("No wallet fetched"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Id of the wallet cannot be empty"));
            }
            var response = await _walletService.DeleteAsync(id);
            return GetResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(WalletVM model)
        {
            if(string.IsNullOrEmpty(model.Name)) 
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            var response = await _walletService.CreateAsync(model);
            return GetResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(WalletVM model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            var response = await _walletService.UpdateAsync(model);
            return GetResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddCardToWallet(WalletVM walletModel, CardVM cardModel)
        {
            if (string.IsNullOrEmpty(walletModel.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(walletModel.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
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
                return GetResult(ServiceResponse.BadRequestResponse("Invalid card expiration date"));
            }
            var response = await _walletService.AddCardToWalletAsync(walletModel, cardModel);
            return GetResult(response);
        }
    }
}
