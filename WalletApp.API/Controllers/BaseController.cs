using Microsoft.AspNetCore.Mvc;
using WalletApp.BLL.Services;

namespace WalletApp.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult GetResult(ServiceResponse serviseResponse)
        {
            return StatusCode((int)serviseResponse.StatusCode, serviseResponse);
        }
    }
}
