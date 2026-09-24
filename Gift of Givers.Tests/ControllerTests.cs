using Gift_of_Givers.Controllers;
using Gift_of_Givers.Data;
using Gift_of_Givers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gift_of_Givers.Tests
{
    public class ControllerTests
    {
        private static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public void AreasOfNeed_Index_ReturnsAllAreasInModel()
        {
            using var context = CreateContext();
            context.AreasOfNeed.AddRange(
                new AreaOfNeed { Name = "Flood Response", Region = "KwaZulu-Natal", Severity = "Critical" },
                new AreaOfNeed { Name = "Drought Relief", Region = "Northern Cape", Severity = "High" });
            context.SaveChanges();

            var controller = new AreasOfNeedController(context);

            var result = Assert.IsType<ViewResult>(controller.Index());
            var model = Assert.IsAssignableFrom<List<AreaOfNeed>>(result.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void AreasOfNeed_Index_WithNoData_ReturnsEmptyList()
        {
            using var context = CreateContext();
            var controller = new AreasOfNeedController(context);

            var result = Assert.IsType<ViewResult>(controller.Index());
            var model = Assert.IsAssignableFrom<List<AreaOfNeed>>(result.Model);
            Assert.Empty(model);
        }

        [Fact]
        public void Donation_Index_ReturnsView()
        {
            using var context = CreateContext();
            var controller = new DonationController(context, null!);

            Assert.IsType<ViewResult>(controller.Index());
        }

        [Fact]
        public void Home_About_ReturnsView()
        {
            var controller = new HomeController(Microsoft.Extensions.Logging.Abstractions.NullLogger<HomeController>.Instance);

            Assert.IsType<ViewResult>(controller.About());
        }
    }
}
