using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchApp.BusinessLayer.DTO;
using SearchApp.BusinessLayer.Infrastructure;
using SearchApp.BusinessLayer.Services;
using SearchApp.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace SearchApp.Web.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ISearchService searchService;

        public SearchController(ILogger<SearchController> logger, ISearchService searchService) : base(logger)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SearchIndexVM searchVM)
        {
            if (ModelState.IsValid)
            {
                var result = searchService.SearchOnline(searchVM.Words); //TODO: enginesIds
                if (result.Succeedeed)
                {
                    SearchResult searchResult = result.Result as SearchResult;
                    IEnumerable<ResultVM> resultVMs = searchResult.Results.Select(r =>
                            new ResultVM
                            {
                                Link = r.Link,
                                Description = r.Description,
                                Title = r.Description
                            }
                        );
                    AllResultsVM resultsVM = new AllResultsVM
                    {
                        Error = "",
                        Results = resultVMs
                    };
                    RedirectToAction("Results", new { results = resultsVM });
                }
                else
                    ModelState.AddModelError(string.Empty, $"{result.Message}\n{result.Result?.ToString()}");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Results(AllResultsVM allResults)
        {
            return View(allResults);
        }
    }
}