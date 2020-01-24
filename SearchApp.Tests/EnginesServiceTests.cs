using Moq;
using SearchApp.BusinessLayer.Infrastructure;
using SearchApp.BusinessLayer.Services;
using SearchApp.DataLayer;
using SearchApp.DataLayer.Entities;
using Xunit;

namespace SearchApp.Tests
{
    public class EnginesServiceTests
    {
        [Fact]
        public void GetEngineTest()
        {
            var uowMock = new Mock<IUnitOfWork>();
            var gEngineMock = new Engine
            {
                Id = 5,
                DescElementSelector = "div .rc .s .st",
                LinkElementSelector = "div .rc .r a",
                Name = "Google",
                QueryUrl = "https://www.google.com/search?q=",
                ResultElementSelector = ".g",
                TitleElementSelector = "div .rc .r a"
            };
            var id = 5;
            uowMock.Setup(u => u.Repository<Engine>().GetById(id, false)).Returns(gEngineMock);

            EnginesService enginesService = new EnginesService(uowMock.Object);
            OperationDetails od = enginesService.GetById(5);
            OperationDetails result = new OperationDetails(true, $"Движок с Id={id} найден", "EnginesService.GetById", EnginesService.MapEngineDTO(gEngineMock));
            Assert.Equal(od, result);
        }
    }
}
