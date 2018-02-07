using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UnitTest.Controllers;
using Xunit;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            HomeController home = new HomeController();
            string result = home.GetEmployeeName(1);
            Assert.Equal("Jignesh", result);
        }
        [Fact]
        public void Test2()
        {
            HomeController home = new HomeController();
            string result = home.GetEmployeeName(1);
            Assert.Equal("Rakesh", result);
        }

        [Theory]
        [InlineData(1, "Jignesh")]
        [InlineData(2, "Rakesh")]
        [InlineData(3, "Not Found")]
        public void Test3(int empId, string name)
        {
            HomeController home = new HomeController();
            string result = home.GetEmployeeName(empId);
            Assert.Equal(name, result);
        }

        [Fact]
        public void Test4()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<TestController>();
            TestController home = new TestController(logger);
            string result = home.GetMessage();
            Assert.Equal("Hi! Reader", result);
        }
    }
}
