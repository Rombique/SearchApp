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
                var enginesList = new List<Engine>
                {
                    new Engine
                    {
                        Name = "Google",
                        QueryUrl = "https://www.google.com/search?q=",
                        TitleElementSelector = "div .rc .r a",
                        DescElementSelector = "div .rc .s .st",
                        LinkElementSelector = "div .rc .r a",
                        ResultElementSelector = ".g"
                    },
                    new Engine
                    {
                        Name = "Яндекс",
                        QueryUrl = "https://yandex.ru/search/?text=",
                        TitleElementSelector = "h2 a",
                        DescElementSelector = "div",
                        LinkElementSelector = "h2 a",
                        ResultElementSelector = ".serp-item"
                    }
                };
                enginesList.ForEach(e => uow.Repository<Engine>().AddNew(e));
                int result = uow.Commit();
            }
        }
    }
}
