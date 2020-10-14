using AutoMapper;
using InitiativesPlus.Application.Services;
using InitiativesPlus.Application.ViewModels;
using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InitiativesPlus.Tests.ServiceTests
{
    [TestClass]
    public class InitiativeTests
    {
        [TestMethod]
        public void GetInitiativeAsyncReturnsOk()
        {
            // Arrange
            var initiative = new InitiativeForCreate
            {
                Year = "2019",
                Name = "Test Initiative"
            };

            var mockInitiativeRepository = new Mock<IInitiativeRepository> { CallBase = true };
            var mockEventRepository = new Mock<IEventRepository> { CallBase = true };
            mockInitiativeRepository.Setup(x => x.InitiativeExistsAsync(initiative.Name, initiative.Year)).ReturnsAsync(true);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<InitiativeForCreate, Initiative>();
            });

            var mapper = config.CreateMapper();
            var service = new InitiativeService(mockInitiativeRepository.Object, mapper, mockEventRepository.Object);
            // Act
            var result =  service.InitiativeExistsAsync(initiative);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Exception);
            Assert.IsTrue(result.Result);
        }
    }
}
