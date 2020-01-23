using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using SearchApp.BusinessLayer.DTO;
using System.Linq;

namespace SearchApp.BusinessLayer.Infrastructure
{
    public class Parser : IParser
    {
        public SearchResult Execute(RequestDTO request)
        {
            var checkRequestErrorResult = CheckRequestDTO(request);
            if (checkRequestErrorResult != null)
                return checkRequestErrorResult;

            var requestUrl = request.Engine.QueryUrl + request.Words;

            string resultElementSelector = request.Engine.ResultElementSelector;
            string titleElementSelector = request.Engine.TitleElementSelector;
            string descElementSelector = request.Engine.DescElementSelector;
            string linkElementSelector = request.Engine.LinkElementSelector;

            var web = new HtmlWeb();
            var doc = web.Load(requestUrl);

            var list = doc.DocumentNode.QuerySelectorAll(resultElementSelector)
                .Select(r => new ResultDTO
                {
                    Title = r.QuerySelector(titleElementSelector)?.InnerText,
                    Link = r.QuerySelector(linkElementSelector)?.GetAttributeValue("href", ""),
                    Description = r.QuerySelector(descElementSelector)?.InnerText
                });

            if (list.Any())
            {
                var errorItems = list.Select(item => CheckResultDTO(item))?.Where(r => r != null && !r.Succeedeed);
                if (errorItems != null && errorItems.Count() == list.Count())
                    return errorItems.First();
                return new SearchResult
                {
                    Results = list.Take(10),
                    EngineId = request.EngineId,
                    Succeedeed = true,
                };
            }

            return new SearchResult
            {
                Succeedeed = false,
                Message = "Execute: Ни одного результата не было получено!"
            };
        }

        private SearchResult CheckRequestDTO(RequestDTO request)
        {
            if (request == null)
                return new SearchResult(false, "CheckRequestDTO: request == null");
            if (request.Engine == null)
                return new SearchResult(false, "CheckRequestDTO: request.Engine == null");
            if (string.IsNullOrEmpty(request.Words))
                return new SearchResult(false, "CheckRequestDTO: request.Words - null или empty");
            if (string.IsNullOrWhiteSpace(request.Engine.QueryUrl) || !request.Engine.QueryUrl.StartsWith("http"))
                return new SearchResult(false, "CheckRequestDTO: request.Engine.QueryUrl неправильного формата");
            if (string.IsNullOrEmpty(request.Engine.DescElementSelector))
                return new SearchResult(false, "CheckRequestDTO: request.Engine.DescElementClass - null или empty");
            if (string.IsNullOrEmpty(request.Engine.LinkElementSelector))
                return new SearchResult(false, "CheckRequestDTO: request.Engine.LinkElementClass - null или empty");
            if (string.IsNullOrEmpty(request.Engine.ResultElementSelector))
                return new SearchResult(false, "CheckRequestDTO: request.Engine.ResultElementSelector - null или empty");
            if (string.IsNullOrEmpty(request.Engine.TitleElementSelector))
                return new SearchResult(false, "CheckRequestDTO: request.Engine.TitleElementSelector - null или empty");
            return null;
        }

        private SearchResult CheckResultDTO(ResultDTO result)
        {
            if (result == null)
                return new SearchResult(false, "CheckResultDTO: result == null");
            if (string.IsNullOrWhiteSpace(result.Link) || !result.Link.StartsWith("http"))
                return new SearchResult(false, "CheckResultDTO: result.Link неправильного формата");
            if (string.IsNullOrEmpty(result.Title))
                return new SearchResult(false, "CheckResultDTO: result.Title - null или empty");
            if (string.IsNullOrEmpty(result.Description))
                return new SearchResult(false, "CheckResultDTO: result.Description - null или empty");
            return null;
        }
    }

    interface IParser
    {
        SearchResult Execute(RequestDTO request);
    }
}
