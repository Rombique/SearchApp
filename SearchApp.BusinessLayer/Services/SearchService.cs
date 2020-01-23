﻿using Microsoft.EntityFrameworkCore;
using SearchApp.BusinessLayer.DTO;
using SearchApp.BusinessLayer.Infrastructure;
using SearchApp.DataLayer;
using SearchApp.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchApp.BusinessLayer.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork unitOfWork;

        public SearchService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public OperationDetails SearchOnline(string words, params int[] enginesIds)
        {
            if (string.IsNullOrEmpty(words))
                return new OperationDetails(false, $"Не задан поисковый запрос", "SearchService.SearchOnline");

            IEnumerable<Engine> enginesList = enginesIds.Length > 0 
                ? unitOfWork.Repository<Engine>().Get(e => enginesIds.Contains(e.Id))
                : unitOfWork.Repository<Engine>().GetAll();

            var searchTasks = enginesList.Select(e => RunSearchTask(e, words)).ToArray();

            int resultIndex = Task.WaitAny(searchTasks, 5000);

            if (resultIndex == -1)
                return new OperationDetails(false, "Время ожидания результатов истекло!", "SearchService.SearchOnline");

            SearchResult firstTaskResult = searchTasks[resultIndex].Result;
            try
            {
                AddNewInfoToDB(firstTaskResult, words);
            }
            catch (DbUpdateException ex)
            {
                return new OperationDetails(false, ex.Message, "SearchService.SearchOnline", ex.InnerException);
            }

            return new OperationDetails(true, "Успешно", "SearchService.SearchOnline", firstTaskResult);
        }

        public OperationDetails SearchLocally(string words)
        {
            throw new System.NotImplementedException();
        }

        private Task<SearchResult> RunSearchTask(Engine eng, string words)
        {
            var engine = new EngineDTO
            {
                DescElementSelector = eng.DescElementSelector,
                LinkElementSelector = eng.LinkElementSelector,
                Name = eng.Name,
                QueryUrl = eng.QueryUrl,
                ResultElementSelector = eng.ResultElementSelector,
                TitleElementSelector = eng.TitleElementSelector
            };
            var request = new RequestDTO
            {
                EngineId = eng.Id,
                Words = words,
                Engine = engine
            };
            return Task.Run(() => new Parser().Execute(request));   
        }

        private void AddNewInfoToDB(SearchResult firstTaskResult, string words)
        {
            Request newRequestEntity = new Request
            {
                DateCreated = DateTime.Now,
                EngineId = firstTaskResult.EngineId,
                Words = words
            };

            unitOfWork.Repository<Request>().AddNew(newRequestEntity);
            unitOfWork.Commit();

            foreach (ResultDTO result in firstTaskResult.Results)
            {
                Result newResult = new Result
                {
                    RequestId = newRequestEntity.Id,
                    Title = result.Title,
                    URL = result.Link,
                    Description = result.Description
                };
                unitOfWork.Repository<Result>().AddNew(newResult);
            }
            unitOfWork.Commit();
        }
    }
    public interface ISearchService
    {
        OperationDetails SearchOnline(string words, params int[] enginesIds);

        OperationDetails SearchLocally(string words);
    }
}