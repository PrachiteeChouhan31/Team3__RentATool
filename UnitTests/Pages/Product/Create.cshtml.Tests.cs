using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Pages.Product.Create
{
    /// <summary>
    /// Created a class to test Create.cshtml.cs
    /// </summary>
    public class CreateTests
    {
        #region TestSetup
        public static CreateModel pageModel;

        /// <summary>
        /// Initializes CreateModel
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new CreateModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup
        //HTTP GET
        /// <summary>
        /// Tests that count of all data is correct when a new product is added
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange
            var oldCount = TestHelper.ProductService.GetAllData().Count();

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(oldCount, TestHelper.ProductService.GetAllData().Count());
        }
        #endregion OnGet
        #region OnPost
        /// <summary>
        /// this test that valid product should add the product in json, return formResult and redirect to index page
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Return_Products()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                ToolName = "title",
                Description = "description",
                Image = "http://www.testing.com",
                Category = "category"
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
            Assert.AreEqual(true, pageModel.FormResult.Contains("Successfully"));
        }

        /// <summary>
        /// Tests an invalid product model and modelstate becomes invalid
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                Id="test",
                ToolName = "bogus",
                Description = "bogus",
                Image = "bogus"
            };
           

            // Force an invalid error state
            
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
          
        }
       
        #endregion OnPost
    }
}