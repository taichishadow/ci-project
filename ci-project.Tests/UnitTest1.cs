using Microsoft.VisualStudio.TestTools.UnitTesting;
using ci_project.Controllers;
using ci_project.Models;
using System.Collections.Generic;
using NSubstitute;
using ci_project.db;

namespace ci_project.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Sqlite.init();
            UserController userContorller = new UserController();
            UserModel users = new UserModel();
            var testResult = userContorller.obtain_users(users);
            Assert.IsNotNull(testResult);
        }
    }
}
