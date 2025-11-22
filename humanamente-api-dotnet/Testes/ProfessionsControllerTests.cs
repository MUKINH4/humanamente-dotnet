using humanamente.Context;
using humanamente.Controllers;
using humanamente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace humanamente.Testes
{
    public class ProfessionsControllerTests
    {
        private AppDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ProfessionsTestsDb")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAll_Should_Return_Ok()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            context.Professions.Add(new Profession { Title = "Professor", Description = "Ensino" });
            await context.SaveChangesAsync();

            var controller = new ProfessionsController(context);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
