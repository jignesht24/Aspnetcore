using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCProject.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using MVCproject.Core;
using MVCproject.Controllers;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mock = new Mock<ILogger<TestController>>();
            var logger = mock.Object;
            TestController home = new TestController(logger);
            string result = home.GetMessage();

            Assert.AreEqual("Hi! Reader", result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var mock = new Mock<IGetDataRepository>();
            mock.Setup(p => p.GetNameById(1)).Returns("Jignesh");
            HomeController home = new HomeController(mock.Object);
            string result = home.GetNameById(1);
            Assert.AreEqual("Jignesh", result);
        }
    }
}
