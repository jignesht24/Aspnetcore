using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unittest.Controllers;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            HomeController home = new HomeController();
            string result = home.GetEmployeeName(1);
            Assert.AreEqual(result, "Jignesh");
        }
        [TestMethod]
        public void TestMethod2()
        {
            HomeController home = new HomeController();
            string result = home.GetEmployeeName(2);
            Assert.AreEqual(result, "Jignesh");
        }
        [TestMethod]
        public void TestMethod4()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<TestController>();
            TestController home = new TestController(logger);
            logger.LogInformation("Test");
            string result = home.GetMessage();
            Assert.AreEqual(result, "Hi! Reader");
        }

    }
}
