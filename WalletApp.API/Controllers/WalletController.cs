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
        [HttpDelete("DeleteWallet")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Id of the wallet cannot be empty"));
            }
            var response = await _walletService.DeleteAsync(id);
            return GetResult(response);
        }
        [HttpPost("CreateWallet")]
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
        [HttpPost("UpdateWallet")]
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
        [HttpPost("AddCardToWallet")]
        public async Task<IActionResult> AddCardToWallet(WalletCardVM model)
        {
            if (string.IsNullOrEmpty(model.Wallet.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Wallet.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            if (string.IsNullOrWhiteSpace(model.Card.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have an id"));
            }
            if (string.IsNullOrEmpty(model.Card.CurrencyCode))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have a currency"));
            }
            if (model.Card.CardNumber.ToString().Length > 16 || model.Card.CardNumber.ToString().Length < 16)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid card number"));
            }
            if (model.Card.ExpirationDate.Year < DateTime.UtcNow.Year && model.Card.ExpirationDate.Month < DateTime.UtcNow.Month)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid card expiration date"));
            }

            /////////
            
            var response = await _walletService.AddCardToWalletAsync(model.Wallet, model.Card);
            return GetResult(response);
        }
        [HttpDelete("DeleteCardFromWallet")]
        public async Task<IActionResult> DeleteCardFromWallet(WalletCardVM model)
        {
            if (string.IsNullOrEmpty(model.Wallet.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Wallet.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            if (string.IsNullOrWhiteSpace(model.Card.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have an id"));
            }
            if (string.IsNullOrEmpty(model.Card.CurrencyCode))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Card must have a currency"));
            }
            if (model.Card.CardNumber.ToString().Length > 16 || model.Card.CardNumber.ToString().Length < 16)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid card number"));
            }
            if (model.Card.ExpirationDate.Year < DateTime.UtcNow.Year && model.Card.ExpirationDate.Month < DateTime.UtcNow.Month)
            {
                return GetResult(ServiceResponse.BadRequestResponse("Invalid card expiration date"));
            }

            /////////////////

            var response = await _walletService.DeleteCardFromWalletAsync(model.Wallet, model.Card);
            return GetResult(response);
        }
        [HttpPost("AddIncomeSourceToWallet")]
        public async Task<IActionResult> AddIncomeSourceToWallet(WalletIncomeSourceVM model)
        {
            if (string.IsNullOrEmpty(model.Wallet.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Wallet.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            if (string.IsNullOrEmpty(model.IncomeSource.Id)){
                return GetResult(ServiceResponse.BadRequestResponse("Income source Id cannot be empty"));
            }
            if (string.IsNullOrEmpty(model.IncomeSource.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Income source name cannot be empty"));
            }
            var response = await _walletService.AddIncomeSourceToWalletAsync(model.Wallet, model.IncomeSource);
            return GetResult(response);
        }
        [HttpDelete("DeleteIncomeSourceFromWallet")]
        public async Task<IActionResult> DeleteIncomeSourceFromWallet(WalletIncomeSourceVM model)
        {
            if (string.IsNullOrEmpty(model.Wallet.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Wallet.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            if (string.IsNullOrEmpty(model.IncomeSource.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Income source Id cannot be empty"));
            }
            if (string.IsNullOrEmpty(model.IncomeSource.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Income source name cannot be empty"));
            }
            var response = await _walletService.DeleteIncomeSourceFromWalletAsync(model.Wallet,model.IncomeSource);
            return GetResult(response);
        }
        [HttpPost("AddSpendingCategoryToWallet")]
        public async Task<IActionResult> AddSpendingCategoryToWallet(WalletSpendingCategoryVM model)
        {
            if (string.IsNullOrEmpty(model.Wallet.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Wallet.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            if (string.IsNullOrEmpty(model.SpendingCategory.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Spending category Id cannot be empty"));
            }
            if (string.IsNullOrEmpty(model.SpendingCategory.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Spending category name cannot be empty"));
            }
            var response = await _walletService.AddSpendingCategoryToWalletAsync(model.Wallet, model.SpendingCategory);
            return GetResult(response);
        }
        [HttpDelete("DeleteSpendingCategoryFromWallet")]
        public async Task<IActionResult> DeleteSpendingCategoryFromWallet(WalletSpendingCategoryVM model)
        {
            if (string.IsNullOrEmpty(model.Wallet.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Wallet.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            if (string.IsNullOrEmpty(model.SpendingCategory.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Spending category Id cannot be empty"));
            }
            if (string.IsNullOrEmpty(model.SpendingCategory.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Spending category name cannot be empty"));
            }
            var response = await _walletService.DeleteSpendingCategoryFromWalletAsync(model.Wallet, model.SpendingCategory);
            return GetResult(response);
        }
    }
}
