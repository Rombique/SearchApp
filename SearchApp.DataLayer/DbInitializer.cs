using SearchApp.DataLayer.Entities;
using System.Collections.Generic;

namespace SearchApp.DataLayer
{
    public class DbInitializer
    {
        public static void InitMain(IUnitOfWork uow)
        {
            var enginesCount = uow.Repository<Engine>().GetCount(null, true);
            if (enginesCount == 0)
            {
                var enginesList = new List<Engine>()
                {
                    new Engine() { Name = "TestEngine" }
                };
                enginesList.ForEach(e => uow.Repository<Engine>().AddNew(e));
                int result = uow.Commit();
            }
        }
    }
}
