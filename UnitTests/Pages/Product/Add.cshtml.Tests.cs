using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.AddTests
{
    
    /// <summary>
    /// Created a class to test Create.cshtml.cs
    /// </summary>
    public class AddTests
    {
        #region TestSetup
        public static Add pageModel;

        /// <summary>
        /// Initializes CreateModel
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new Add(TestHelper.ProductService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Product()
        {
            // Arrange

            // Act
            pageModel.OnGet("2");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Hammer", pageModel.Product.ToolName);
        }

        /// <summary>
        /// Tests that count of all data is correct when a new product is added
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange
            var oldCount = TestHelper.ProductService.GetAllData().Count();

            // Act
            pageModel.OnGet("99");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(oldCount, TestHelper.ProductService.GetAllData().Count());
        }

        /// <summary>
        /// Tests that invalid product redirects to index page
        /// </summary>
        [Test]
        public void OnGet_Invalid_No_Match_Should_Redirect_To_Index()
        {
            // Arrange

            // Act
            var result = pageModel.OnGet("ABCD1234") as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// Tests that null product redirects to index page
        /// </summary>
        [Test]
        public void OnGet_Invalid_Null_Should_Redirect_To_Index()
        {
            // Arrange

            // Act
            var result = pageModel.OnGet(null) as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }
        #endregion OnGet

        /// <summary>
        /// Tests a valid update and checks page redirects to index page after
        /// </summary>
        #region OnPostAsync
        [Test]
        public void OnPostAsync_Valid_Should_Return_Products()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                ToolName = "title",
                Description = "description",
                Image = "image",
                Category = "category"
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// Tests an invalid product model
        /// </summary>
        [Test]
        public void OnPostAsync_InValid_Model_NotValid_Return_Page()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
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
        #endregion OnPostAsync
    }
}