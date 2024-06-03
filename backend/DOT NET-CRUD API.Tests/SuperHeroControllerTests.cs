using DOT_NET_CRUD_API.Controllers;
using DOT_NET_CRUD_API.Data;
using DOT_NET_CRUD_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DOT_NET_CRUD_API.Tests
{
    public class SuperHeroControllerTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly Mock<DbSet<SuperHero>> _mockSet;
        private readonly SuperHeroController _controller;

        public SuperHeroControllerTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockSet = new Mock<DbSet<SuperHero>>();

            _mockContext.Setup(m => m.SuperHeroes).Returns(_mockSet.Object);

            _controller = new SuperHeroController(_mockContext.Object);
        }

        private void SetUpDbSet(IEnumerable<SuperHero> data)
        {
            var queryableData = data.AsQueryable();
            _mockSet.As<IQueryable<SuperHero>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            _mockSet.As<IQueryable<SuperHero>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            _mockSet.As<IQueryable<SuperHero>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            _mockSet.As<IQueryable<SuperHero>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
        }

        [Fact]
        public async Task GetAllHeroes_ReturnsAllHeroes()
        {
            // Arrange
            var mockHeroes = new List<SuperHero>
            {
                new SuperHero { Id = 1, Name = "Hero1", FirstName = "FirstName1", LastName = "LastName1", Place = "Place1" },
                new SuperHero { Id = 2, Name = "Hero2", FirstName = "FirstName2", LastName = "LastName2", Place = "Place2" }
            };
            SetUpDbSet(mockHeroes);

            // Act
            var result = await _controller.GetAllHeroes();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<SuperHero>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualHeroes = Assert.IsType<List<SuperHero>>(okResult.Value);
            var actualHeroesCount = actualHeroes.Count;
            Assert.Equal(2, actualHeroesCount);
        }

        [Fact]
        public async Task GetAllHeroes_EmptyList()
        {
            // Arrange
            SetUpDbSet([]);

            // Act
            var result = await _controller.GetAllHeroes();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<SuperHero>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualHeroes = Assert.IsType<List<SuperHero>>(okResult.Value);
            var actualHeroesCount = actualHeroes.Count;
            Assert.Empty(actualHeroes);
        }
    }
}