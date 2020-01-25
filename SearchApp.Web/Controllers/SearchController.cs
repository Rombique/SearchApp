using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchApp.BusinessLayer.Infrastructure;
using SearchApp.BusinessLayer.Services;
using SearchApp.Web.Extensions;
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
                var result = !searchVM.IsOfflineSearch ? searchService.SearchOnline(searchVM.Words, 1, 3) : searchService.SearchLocally(searchVM.Words); //TODO: enginesIds
                logger.LogInformation(result.Message);
                if (result.Succeedeed)
                {
                    SearchResult searchResult = result.Result as SearchResult;
                    List<ResultVM> resultVMs = searchResult.Results.Select(r =>
                            new ResultVM
                            {
                                Link = r.Link,
                                Description = r.Description,
                                Title = r.Title
                            }
                        ).ToList();
                    AllResultsVM allResults = new AllResultsVM
                    {
                        Error = "",
                        Results = resultVMs,
                        Words = searchVM.Words,
                        EngineName = searchResult.EngineName
                    };
                    TempData.Put("allResults", allResults);
                    return RedirectToAction("Results");
                }
                else
                    ModelState.AddModelError(string.Empty, $"{result.Message}\n{result.Result?.ToString()}");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Results()
        {
            AllResultsVM allResults = TempData.Get<AllResultsVM>("allResults");
            return View(allResults);
        }
    }
}