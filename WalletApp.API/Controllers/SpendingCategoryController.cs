using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Services.IncomeSourceService;
using WalletApp.BLL.Services;
using WalletApp.DAL.ViewModels;
using WalletApp.BLL.Services.SpendingCategoryService;
using WalletApp.DAL.Models;

namespace WalletApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpendingCategoryController : BaseController
    {
        private readonly ISpendingCategoryService _spendingCategoryService;
        public SpendingCategoryController(ISpendingCategoryService spendingCategoryService)
        {
            _spendingCategoryService = spendingCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string? id, string? name)
        {
            id = Request.Query[nameof(id)];
            name = Request.Query[nameof(name)];

            if (id == null && name == null)
            {
                var response = await _spendingCategoryService.GetAllAsync();
                return GetResult(response);
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                var response = await _spendingCategoryService.GetByIdAsync(id);
                if (response.Success)
                {
                    return GetResult(response);
                }
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                var response = await _spendingCategoryService.GetByNameAsync(name);
                if (response.Success)
                {
                    return GetResult(response);
                }
            }
            return GetResult(ServiceResponse.BadRequestResponse("No income source fetched"));

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Id of the income source cannot be empty"));
            }
            var response = await _spendingCategoryService.DeleteAsync(id);
            return GetResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(SpendingCategoryVM model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            var response = await _spendingCategoryService.CreateAsync(model);
            return GetResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(SpendingCategoryVM model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            var response = await _spendingCategoryService.UpdateAsync(model);
            return GetResult(response);
        }
    }
}
