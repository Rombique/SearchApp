using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchApp.BusinessLayer.DTO;
using SearchApp.BusinessLayer.Services;
using SearchApp.Web.Extensions;
using SearchApp.Web.Models;

namespace SearchApp.Web.Controllers
{
    public class EnginesController : BaseController
    {
        private readonly IEnginesService enginesService;
        public EnginesController(ILogger<BaseController> logger, IEnginesService enginesService) : base(logger)
        {
            this.enginesService = enginesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allEnginesOD = enginesService.GetAll();
            if (allEnginesOD.Succeedeed)
            {
                var engineDTOs = allEnginesOD.Result as IEnumerable<EngineDTO>;
                var results = engineDTOs.Select(e => GetEngineVM(e)).ToList();
                return View(results);
            }
            return View();
        }

        private EngineVM GetEngineVM(EngineDTO engine) =>
            new EngineVM
            {
                DescElementSelector = engine.DescElementSelector,
                LinkElementSelector = engine.LinkElementSelector,
                Name = engine.Name,
                QueryUrl = engine.QueryUrl,
                ResultElementSelector = engine.ResultElementSelector,
                TitleElementSelector = engine.TitleElementSelector,
                Id = engine.Id
            };
    }
}