using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using WebApplication1.Controllers;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        public Mock<HttpServerUtilityBase> Server { get; private set; }
        public Mock<HttpContextBase> Http { get; private set; }
        public Mock<HttpSessionStateBase> Session { get; private set; }


        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["login"]).Returns("1");
            controller.ControllerContext = controllerContext.Object;
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
       

        [TestMethod]
        public void FileUploadSuccessful()
        {
           
            // Arrange
            HomeController controller = new HomeController();
            Http = new Mock<HttpContextBase>(MockBehavior.Loose);
            Session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
            Server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);            
            Http.Setup(c => c.Session["login"]).Returns("1");
           // Http.SetupGet(c => c.Server).Returns(Server.Object);
            Http.Setup(c => c.Server.MapPath("~/UploadedFiles")).Returns("UploadedFiles");

            controller.ControllerContext =  new ControllerContext
            {
                HttpContext = Http.Object,
            };

            // Act

            string filePath = Path.GetFullPath(@"../../TestFiles/Alladin.jpg");
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                uploadedFile
                    .Setup(f => f.ContentLength)
                    .Returns(10);

                uploadedFile
                    .Setup(f => f.FileName)
                    .Returns("testimage.jpg");

                uploadedFile
                    .Setup(f => f.InputStream)
                    .Returns(fileStream);
            }

            ViewResult result = controller.Index(uploadedFile.Object) as ViewResult;
            Assert.AreEqual("File Uploaded Successfully!!", result.ViewBag.Message);
        }

        [TestMethod]
        public void FileUploadUnSuccessful()
        {
            // Arrange
            HomeController controller = new HomeController();
            Http = new Mock<HttpContextBase>(MockBehavior.Loose);
            Session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
            Server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            Http.Setup(c => c.Session["login"]).Returns("1");
            // Http.SetupGet(c => c.Server).Returns(Server.Object);
            Http.Setup(c => c.Server.MapPath("~/UploadedFiles")).Returns("UploadedFiles");

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = Http.Object,
            };
            // Act


            string filePath = Path.GetFullPath(@"../../TestFiles/Alladin.jpg");
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                uploadedFile
                    .Setup(f => f.ContentLength)
                    .Returns(0);

                uploadedFile
                    .Setup(f => f.FileName)
                    .Returns("testimage.jpg");

                uploadedFile
                    .Setup(f => f.InputStream)
                    .Returns(fileStream);
            }

            ViewResult result = controller.Index(uploadedFile.Object) as ViewResult;
            Assert.AreEqual("File upload failed!!", result.ViewBag.Message);
        }
    }
}
