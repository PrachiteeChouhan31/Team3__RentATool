using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.Return
{
    /// <summary>
    /// Created a class to test Return.cshtml.cs
    /// </summary>
    public class ReturnTests
    {
        #region TestSetup
        public static ReturnModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new ReturnModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup
        //Test HTTP GET to return valid product
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("2");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Hammer", pageModel.Product.ToolName);
        }
        /// <summary>
        /// Test HTTP GET for invalid product id
        /// </summary>


        [Test]
        public void OnGet_InValid_Should_Return_ToolPage()
        {
            // Arrange

            // Act
            var result = pageModel.OnGet("test") as RedirectToPageResult;

            // Assert

            Assert.AreEqual(true, result.PageName.Contains("ToolPage"));
        }
        #endregion OnGet

        #region OnPostAsync

        /// <summary>
        /// this class test HTTP POST should return valid product
        /// </summary>
        [Test]
       
        public void OnPostAsync_Valid_Should_Return_Products()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                Id ="2",
            };
            
            pageModel.Info = new ReturnModel.ReturnInfo 
            {
                Firstname="prachitee",
                Lastname ="chouhan",
                Email="pchouhan@seattleu.edu"

            };
            var RentalInfo = new Rental
            {
                FirstName = "prachitee",
                LastName = "chouhan",
                Email = "pchouhan@seattleu.edu",
                PhoneNumber = "555-555-5555",
                RentalDate = "01/01/2022"

            };
            pageModel.ProductService.AddUserInfoRent("2",RentalInfo);//first rent the product before returning
            
            // Act
            var result = pageModel.OnPost(pageModel.Info) as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("ToolPage"));
            Assert.AreEqual(true, pageModel.FormResult.Contains("Successfully"));
        }
        /// <summary>
        /// test HTTP to retuen invalid product
        /// </summary>

        [Test]
        public void OnPostAsync_InValid_Model_NotValid_Return_Page()
        {
            // Arrange
            pageModel.Info = new ReturnModel.ReturnInfo
            {
                Firstname = "bogus",
                Lastname = "bogus",
                Email = "bogus"
            };
            

            // Force an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            var result = pageModel.OnPost(pageModel.Info) as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// test HTTP to return invalid product
        /// </summary>

        [Test]
        public void OnPostAsync_InValid_Product_NotValid_Return_Page()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                Id = "testNull",
            };

            pageModel.Info = new ReturnModel.ReturnInfo
            {
                Firstname = "bogus",
                Lastname = "bogus",
                Email = "bogus"
            };

            // Act
            var result = pageModel.OnPost(pageModel.Info) as ActionResult;

            // Assert
            string test = "Can't process your return, please provide valid information and try again.";
            Assert.AreEqual(test, pageModel.FormResult);
        }
        #endregion OnPostAsync
    }
}