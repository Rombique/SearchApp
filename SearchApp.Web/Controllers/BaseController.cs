using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SearchApp.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> logger;

        public BaseController(ILogger<BaseController> logger)
        {
            this.logger = logger;
        }
    }
}
