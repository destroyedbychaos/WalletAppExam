using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WalletApp.BLL.Services;
using WalletApp.BLL.Services.WalletService;
using WalletApp.BLL.Validators;
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
            var validator = new WalletValidator();
            var validateResult = await validator.ValidateAsync(model);
            if (validateResult.IsValid)
            {
                var response = await _walletService.CreateAsync(model);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse(validateResult.Errors.First().ErrorMessage));

        }
        [HttpPost("UpdateWallet")]
        public async Task<IActionResult> UpdateAsync(WalletVM model)
        {
            var validator = new WalletValidator();
            var validateResult = await validator.ValidateAsync(model);
            if (validateResult.IsValid)
            {
                var response = await _walletService.UpdateAsync(model);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse(validateResult.Errors.First().ErrorMessage));


        }
        [HttpPost("AddCardToWallet")]
        public async Task<IActionResult> AddCardToWallet(WalletCardVM model)
        {
            var validatorWallet = new WalletValidator();
            var validatorCard = new CardValidator();
            var validateResultWallet = await validatorWallet.ValidateAsync(model.Wallet);
            var validateResultCard = await validatorCard.ValidateAsync(model.Card);

            if(validateResultWallet.IsValid && validateResultCard.IsValid)
            {
                var response = await _walletService.AddCardToWalletAsync(model.Wallet, model.Card);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse(validateResultWallet.Errors.First().ErrorMessage + validateResultCard.Errors.First().ErrorMessage));
            

        }
        [HttpDelete("DeleteCardFromWallet")]
        public async Task<IActionResult> DeleteCardFromWallet(WalletCardVM model)
        {
            var validatorWallet = new WalletValidator();
            var validatorCard = new CardValidator();
            var validateResultWallet = await validatorWallet.ValidateAsync(model.Wallet);
            var validateResultCard = await validatorCard.ValidateAsync(model.Card);

            if (validateResultWallet.IsValid && validateResultCard.IsValid)
            {
                var response = await _walletService.DeleteCardFromWalletAsync(model.Wallet, model.Card);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse(validateResultWallet.Errors.First().ErrorMessage + validateResultCard.Errors.First().ErrorMessage));


        }
        [HttpPost("AddIncomeSourceToWallet")]
        public async Task<IActionResult> AddIncomeSourceToWallet(WalletIncomeSourceVM model)
        {
            var validatorWallet = new WalletValidator();
            var validatorIncomeSource = new IncomeSourceValidator();
            var validateResultWallet = await validatorWallet.ValidateAsync(model.Wallet);
            var validateResultIncomeSource = await validatorIncomeSource.ValidateAsync(model.IncomeSource);

            if (validateResultWallet.IsValid && validateResultIncomeSource.IsValid)
            {
                var response = await _walletService.AddIncomeSourceToWalletAsync(model.Wallet, model.IncomeSource);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse(validateResultWallet.Errors.First().ErrorMessage + validateResultIncomeSource.Errors.First().ErrorMessage));
        }
        [HttpDelete("DeleteIncomeSourceFromWallet")]
        public async Task<IActionResult> DeleteIncomeSourceFromWallet(WalletIncomeSourceVM model)
        {
            var validatorWallet = new WalletValidator();
            var validatorIncomeSource = new IncomeSourceValidator();
            var validateResultWallet = await validatorWallet.ValidateAsync(model.Wallet);
            var validateResultIncomeSource = await validatorIncomeSource.ValidateAsync(model.IncomeSource);

            if (validateResultWallet.IsValid && validateResultIncomeSource.IsValid)
            {
            var response = await _walletService.DeleteIncomeSourceFromWalletAsync(model.Wallet,model.IncomeSource);
            return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse(validateResultWallet.Errors.First().ErrorMessage + validateResultIncomeSource.Errors.First().ErrorMessage));

        }
        [HttpPost("AddSpendingCategoryToWallet")]
        public async Task<IActionResult> AddSpendingCategoryToWallet(WalletSpendingCategoryVM model)
        {
            var validatorWallet = new WalletValidator();
            var validatorSpendingCategory = new SpendingCategoryValidator();
            var validateResultWallet = await validatorWallet.ValidateAsync(model.Wallet);
            var validateResultSpendingCategory = await validatorSpendingCategory.ValidateAsync(model.SpendingCategory);

            if (validateResultWallet.IsValid && validateResultSpendingCategory.IsValid)
            {
                var response = await _walletService.AddSpendingCategoryToWalletAsync(model.Wallet, model.SpendingCategory);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse(validateResultWallet.Errors.First().ErrorMessage + validateResultSpendingCategory.Errors.First().ErrorMessage));
        }
        [HttpDelete("DeleteSpendingCategoryFromWallet")]
        public async Task<IActionResult> DeleteSpendingCategoryFromWallet(WalletSpendingCategoryVM model)
        {
            var validatorWallet = new WalletValidator();
            var validatorSpendingCategory = new SpendingCategoryValidator();
            var validateResultWallet = await validatorWallet.ValidateAsync(model.Wallet);
            var validateResultSpendingCategory = await validatorSpendingCategory.ValidateAsync(model.SpendingCategory);

            if (validateResultWallet.IsValid && validateResultSpendingCategory.IsValid)
            {
                var response = await _walletService.DeleteSpendingCategoryFromWalletAsync(model.Wallet, model.SpendingCategory);
                return GetResult(response);
            }
            return GetResult(ServiceResponse.BadRequestResponse(validateResultWallet.Errors.First().ErrorMessage + validateResultSpendingCategory.Errors.First().ErrorMessage));

        }
    }
}
