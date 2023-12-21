using Microsoft.EntityFrameworkCore;
using Moq;
using n5.Application.Handlers;
using n5.Application.Queries;
using n5.Domain.Entities;
using n5.Infrastructure;
using Nest;
using Xunit;

namespace n5.Test
{
    [TestClass]
    public class TestHandler
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [Fact]
        public async Task Handle_ShouldReturnResultsFromDbContext_WhenSearchTermIsNotProvided()
        {
            // Arrange
            var permissions = new List<Permission> { /* llenar con datos de prueba */ };
            var mockDbSet = new Mock<DbSet<Permission>>();
            mockDbSet.As<IQueryable<Permission>>().Setup(m => m.Provider).Returns(permissions.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Permission>>().Setup(m => m.Expression).Returns(permissions.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Permission>>().Setup(m => m.ElementType).Returns(permissions.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Permission>>().Setup(m => m.GetEnumerator()).Returns(permissions.AsQueryable().GetEnumerator());

            var mockDbContext = new Mock<n5DbContext>();
            mockDbContext.Setup(db => db.Permissions).Returns(mockDbSet.Object);

            var mockElasticClient = new Mock<IElasticClient>();

            var handler = new GetPermissionsHandler(mockDbContext.Object, mockElasticClient.Object);

            // Act
            var result = await handler.Handle(new GetPermissionsQuery { SearchTerm = false }, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.Any());

        }


    }
}