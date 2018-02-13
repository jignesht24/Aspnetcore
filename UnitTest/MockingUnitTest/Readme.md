## Moq: Unit Test .net core App using mock object

### Introduction
The Unit test is a block of code that help us in verifying the expected behaviour of the other code in isolation i.e. there is no dependency between the tests. This is good way to test the application code before it goes for quality assurance (QA). There are three different test frameworks for Unit Testing supported by ASP.NET Core: MSTest, xUnit, and NUnit. All Unit test frameworks, offer a similar end goal and help us to write unit tests that are simpler, easier and faster. 

In my previous articles, I have explain about how to write unit test with different frameworks (i.e. MSTest, xUnit, and NUnit). These areticles having very simple and very straightforward example but real world, class's constructor may have complex object and injected as dependency. In this case, we can create mocked object and use it for unit test.

The mock object is object that can act as a real object but can be controlled in test code. Moq (https://github.com/moq/moq4) is a library that allows us to create mock object in test code. It is also available in NuGet(https://www.nuget.org/packages/Moq/). This library also supports .net core.

The Moq library can add to test project either by package manager or .net CLI tool.

Using Package Manager
```
PM> Install-Package Moq
```
Using .net CLI
```
dotnet add package Moq
```
Following Example, controller class required constructor dependency to create the instance. 

MVCProject/Core/IGetDataRepository.cs
```
namespace MVCproject.Core
{
    public interface IGetDataRepository
    {
        string GetNameById(int id);
    }
} 
```
MVCProject\Infrastructure\EmployeeRepository.cs
```
using MVCproject.Core;
namespace MVCproject.Infrastructure
{
    public class EmployeeRepository : IGetDataRepository
    {
        public string GetNameById(int id)
        {
            string name;
            if (id == 1)
            {
                name = "Jignesh";
            }
            else if (id == 2)
            {
                name = "Rakesh";
            }
            else
            {
                name = "Not Found";
            }
            return name;
        }
    }
}
```
MVCProject\Controllers\HomeController.cs
```
using Microsoft.AspNetCore.Mvc;
using MVCproject.Core;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetDataRepository _data;

        public HomeController(IGetDataRepository data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetNameById(int id)
        {
            return _data.GetNameById(id);
        }
    }
}
```
Here To create controller class instance, we required object of IGetDataRepository. Without a mock object, we need to create object of IGetDataRepository which is real.

Moq library allows us to manipulate mock object in many ways such as setting mock methods to return specific values, setting up required properties, and match the specific arguments when test method is called mock object.

Moq can create a mock version of IGetDataRepository. To demostrate the code, I am using MSTest framework. In this example, I am using Setup and Returns methods to create mock object. The Setup method is used to tell the mock object, how to behave when it calls by test method and Returns methods return specific value.

MockingUnitTest\MSTestProject\UnitTest1.cs
```
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
```
The Moq library is same for all the unit test framework. It means that above test will work with XUnit and NUnit after some syntax modification.

### Summary
The Unit test is code where we test all the code paths of the methods and ensured that the results are as expected. The mock object is object that can act as a real object but can be controlled in test code. It is very useful in generating to generete the objects which are used in test method.
