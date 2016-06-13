using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cafeen.Controllers;
using System.Web.Mvc;

namespace Cafeentest.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestDefaultView()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
    }
    [TestClass]
    public class AccountingsControllerTest
    {
        [TestMethod]
        public void TestCreate()
        {
            var controller = new AccountingsController();
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }


        //[TestMethod]
        //public void TestLock()
        //{
        //    var controller = new AccountingsController();
        //    var Accounting = new Accounting();
        //    var result = controller.Lock(1) as 
        //}



        //[TestMethod]
        //public void testEditAccounting()
        //{
        //    var controller = new AccountingsController();
        //    var accounting = new Accounting();
        //    var Id = 4;
        //    var StartCash = 1000;
        //    var EndCash = 500;
        //    var CardCash = 2500;
        //    var TimeStamp = System.DateTime.Now;
        //    var LockStatus = false;
        //    var StartProduct = "bob";
        //    var EndProduct = "bobelina";
        //    var result = (RedirectToRouteResult) controller.Edit(accounting);
        //    Assert.AreEqual("Index", result.Values["action"]);
        //}
    }
}

