﻿using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Services;
using WalletApp.BLL.Services.IncomeSourceService;
using WalletApp.DAL.Repositories.IncomeSourceRepository;
using WalletApp.DAL.ViewModels;

namespace WalletApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeSourceController : BaseController
    {
        private readonly IIncomeSourceService _incomeSourceService;
        public IncomeSourceController(IIncomeSourceService incomeSourceService)
        {
            _incomeSourceService = incomeSourceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string? id, string? name)
        {
            id = Request.Query[nameof(id)];
            name = Request.Query[nameof(name)];

            if (id == null && name == null)
            {
                var response = await _incomeSourceService.GetAllAsync();
                return GetResult(response);
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                var response = await _incomeSourceService.GetByIdAsync(id);
                if (response.Success)
                {
                    return GetResult(response);
                }
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                var response = await _incomeSourceService.GetByNameAsync(name);
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
            var response = await _incomeSourceService.DeleteAsync(id);
            return GetResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(IncomeSourceVM model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            var response = await _incomeSourceService.CreateAsync(model);
            return GetResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(IncomeSourceVM model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet cannot be unnamed"));
            }
            if (string.IsNullOrEmpty(model.Id))
            {
                return GetResult(ServiceResponse.BadRequestResponse("Wallet Id cannot be empty"));
            }
            var response = await _incomeSourceService.UpdateAsync(model);
            return GetResult(response);
        }
    }
}