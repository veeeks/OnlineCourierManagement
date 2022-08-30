using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CourierManagement.MVC.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace CourierManagement.Tests.Controller
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddEdit_View()
        {
            var controller = new AdministratorsController();
            var result =  controller.AddCities() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeliveryExecutive_View()
        {
            var controller = new DeliveryExecutivesController();
            var result = controller.Home() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void User_View()
        {
            var controller = new UsersController();
            var result = controller.Home() as ViewResult;
            Assert.IsNotNull(result);
        }

    }
}
