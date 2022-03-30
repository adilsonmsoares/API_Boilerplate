using System;
using System.Threading.Tasks;
using AutoFixture;
using Core.Services.Contracts.Example;
using Core.Services.Implementations.Example;
using Data.Repositories.Contracts.Example;
using FluentAssertions;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models.Example;
using WebAPI.Tests.Extensions;
using Xunit;
using ExampleEntity = Domain.Entities.Models.Example.Example;

namespace WebAPI.Tests.Scenarios.Example
{
    public class ExampleTest : BaseTest
    {
        private readonly Mock<IExampleRepository> _exampleRepository;
        private readonly IExampleService _exampleService;
        private readonly ExampleController _exampleController;

        public ExampleTest() : base()
        {
            _exampleRepository = new Mock<IExampleRepository>();
            _exampleService = new ExampleService(Mapper, BaseService, _exampleRepository.Object);
            _exampleController = new ExampleController(Mapper, _exampleService);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(5)]
        public void Index_GetAll_ShouldReturnAList(int length)
        {
            // Arrange
            MockIndex(length);

            // Act
            var result = _exampleController.Index().AsList<ExampleModel>();

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Count.Should().Be(length);
        }

        [Fact]
        public async Task AddAsync_ValidModel_ShouldReturnOk()
        {
            // Arrange
            MockAddAsync();
            var data = Fixture.Create<ExampleModel>();

            // Act
            var result = (await _exampleController.AddAsync(data)).AsDefault();

            // Assert
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AddAsync_InvalidModel_ShouldThrowException(string propertyValue)
        {
            // Arrange
            MockAddAsync();
            var data = Fixture.Build<ExampleModel>()
                .With(e => e.Property, propertyValue)
                .Create();

            // Act
            Func<Task> result = async () => await _exampleController.AddAsync(data);

            // Assert
            result.Should().Throw<ArgumentException>();
        }

        #region Private Methods

        private void MockIndex(int length)
        {
            var model = Fixture.CreateMany<ExampleEntity>(length);
            _exampleRepository.Setup(x => x.GetForExample()).Returns(model);
        }

        private void MockAddAsync()
        {
            _exampleRepository.Setup(x => x.AddAsync(It.IsAny<ExampleEntity>())).ReturnsAsync(It.IsAny<object>());
        }

        #endregion
    }
}
