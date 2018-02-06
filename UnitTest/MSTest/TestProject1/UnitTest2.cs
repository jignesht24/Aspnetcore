using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unittest.Controllers;

namespace TestProject1
{
    [TestClass]
    public class UnitTest2
    {
        [DataTestMethod]
        [DataRow(1, "Jignesh")]
        [DataRow(2, "Rakesh")]
        [DataRow(3, "Not Found")]
        public void TestMethod3(int empId, string name)
        {
            HomeController home = new HomeController();
            string result = home.GetEmployeeName(empId);
            Assert.AreEqual(result, name);
        }
    }
}
