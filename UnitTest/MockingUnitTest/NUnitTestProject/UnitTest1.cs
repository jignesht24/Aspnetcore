using Microsoft.Extensions.Logging;
using Moq;
using MVCproject.Controllers;
using MVCproject.Core;
using MVCProject.Controllers;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var mock = new Mock<ILogger<TestController>>();
            var logger = mock.Object;
            TestController home = new TestController(logger);
            string result = home.GetMessage();

            Assert.AreEqual("Hi! Reader", result);
        }

        [Test]
        public void Test2()
        {
            var mock = new Mock<IGetDataRepository>();
            mock.Setup(p => p.GetNameById(1)).Returns("Jignesh");
            HomeController home = new HomeController(mock.Object);
            string result = home.GetNameById(1);
            Assert.AreEqual("Jignesh", result);
        }
    }
}