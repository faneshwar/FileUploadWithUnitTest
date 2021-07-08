using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        public Mock<HttpServerUtilityBase> Server { get; private set; }
        public Mock<HttpContextBase> Http { get; private set; }
        public Mock<HttpSessionStateBase> Session { get; private set; }
        [TestMethod]
        public void Index()
        {
            // Arrange
            LoginController controller = new LoginController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ValidUser()
        {
            LoginController controller = new LoginController();
            Http = new Mock<HttpContextBase>(MockBehavior.Loose);
            Session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);        
            Http.Setup(c => c.Session["login"]).Returns("");         

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = Http.Object,
            };


            UserModel user = new UserModel
            {
                UserName = "test1",
                Password = "Test@123"
            };
            var result = controller.Index(user);

            Assert.IsTrue(Boolean.Parse(controller.ViewBag.Validuser.ToString()));
        }

        [TestMethod]
        public void InvalidUser()
        {
            LoginController controller = new LoginController();
            UserModel user = new UserModel
            {
                UserName = "test",
                Password = "Test@1234"
            };
            var result = controller.Index(user);

            Assert.IsFalse(Boolean.Parse(controller.ViewBag.Validuser.ToString()));
        }

    }
}
