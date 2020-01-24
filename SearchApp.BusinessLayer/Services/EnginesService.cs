using Microsoft.EntityFrameworkCore;
using SearchApp.BusinessLayer.DTO;
using SearchApp.BusinessLayer.Infrastructure;
using SearchApp.DataLayer;
using SearchApp.DataLayer.Entities;
using SearchApp.DataLayer.Repositories;
using System.Linq;

namespace SearchApp.BusinessLayer.Services
{
    public class EnginesService : IEnginesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBaseRepository<Engine> enginesRepository;

        public EnginesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.enginesRepository = unitOfWork.Repository<Engine>();
        }

        public OperationDetails Add(EngineDTO engine)
        {
            var entity = enginesRepository.GetFirst(e => e.Name == engine.Name);
            if (entity == null)
            {
                entity = new Engine()
                {
                    Name = engine.Name, //TODO:
                    DescElementSelector = engine.DescElementSelector,
                    QueryUrl = engine.QueryUrl,
                    ResultElementSelector = engine.ResultElementSelector,
                    TitleElementSelector = engine.TitleElementSelector
                };
                try
                {
                    enginesRepository.AddNew(entity);
                    unitOfWork.Commit();
                    return new OperationDetails(true, $"Поисковый движок успешно добавлен!", "EnginesService.Add");
                }
                catch (DbUpdateException ex)
                {
                    return new OperationDetails(false, ex.Message, "EnginesService.Add", ex.InnerException.Message);
                }
            }
            else
            {
                return new OperationDetails(false, $"Поисковый движок с названием \"{engine.Name}\" уже существует!", "EnginesService.Add");
            }
        }

        public OperationDetails GetById(int id)
        {
            var engine = enginesRepository.GetFirst(p => p.Id == id);
            if (engine == null)
                return new OperationDetails(false, $"Движок с Id={id} не найден", "EnginesService.GetById");

            var engineDTO = MapEngineDTO(engine);

            return new OperationDetails(true, $"Движок с Id={id} найден", "EnginesService.GetById", engineDTO);
        }

        public OperationDetails Update(EngineDTO engine)
        {
            var entity = enginesRepository.GetFirst(e => e.Id == engine.Id);
            if (entity == null)
            {
                return new OperationDetails(false, $"Ошибка, поисковый движок с Id={engine.Id} не найден!", "EnginesService.Update");
            }
            else
            {
                entity.Name = engine.Name; //TODO: 
                entity.DescElementSelector = engine.DescElementSelector;
                entity.QueryUrl = engine.QueryUrl;
                entity.ResultElementSelector = engine.ResultElementSelector;
                entity.TitleElementSelector = engine.TitleElementSelector;

                try
                {
                    unitOfWork.Commit();
                    return new OperationDetails(true, $"Поисковый движок успешно обновлен!", "EnginesService.Update");
                }
                catch (DbUpdateException ex)
                {
                    return new OperationDetails(false, ex.Message, "EnginesService.Update", ex.InnerException);
                }
            }
        }

        public OperationDetails DeleteById(int id)
        {
            if (id == 0)
                return new OperationDetails(false, "Нельзя удалить сущность с Id=0", "EnginesService.DeleteById");

            try
            {
                enginesRepository.Delete(e => e.Id == id);
                unitOfWork.Commit();
                return new OperationDetails(true, $"Поисковый движок c Id={id} успешно удален!", "EnginesService.DeleteById");
            }
            catch (DbUpdateException ex)
            {
                return new OperationDetails(false, ex.Message, "EnginesService.DeleteById", ex.InnerException.Message);
            }
        }

        public OperationDetails GetAll()
        {
            var entities = enginesRepository.GetAll(true);
            if (entities == null || entities.Count() == 0)
                return new OperationDetails(false, $"Ошибка, не найден ни один поисковый движок!", "EnginesService.GetAll");

            var result = entities.Select(e => MapEngineDTO(e)).ToList();

            return new OperationDetails(true, "Успех", "EnginesService.GetAll", result);
        }

        private EngineDTO MapEngineDTO(Engine engine) => new EngineDTO()
        {
            Id = engine.Id,
            DescElementSelector = engine.DescElementSelector,
            TitleElementSelector = engine.TitleElementSelector,
            Name = engine.Name,
            QueryUrl = engine.QueryUrl,
            ResultElementSelector = engine.ResultElementSelector,
            LinkElementSelector = engine.LinkElementSelector
        };
    }

    public interface IEnginesService
    {
        OperationDetails Add(EngineDTO engine);
        OperationDetails GetById(int id);
        OperationDetails Update(EngineDTO engine);
        OperationDetails DeleteById(int id);
        OperationDetails GetAll();
    }
}
