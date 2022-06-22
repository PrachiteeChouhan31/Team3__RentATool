using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.Update
{
    /// <summary>
    /// This class tests the update CRUDi page
    /// </summary>
    public class UpdateTests
    {
        #region TestSetup
        public static UpdateModel pageModel;

        /// <summary>
        /// Initializes UpdateModel
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new UpdateModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Tests that model is valid and returns the correct product when called  
        /// with a valid id
        /// </summary>
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

        [Test]
        public void OnGet_Invalid_No_Match_Should_Redirect_To_Index()
        {
            // Arrange

            // Act
            var result = pageModel.OnGet("ABCD1234") as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

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
                Id="3",
                ToolName = "Table Saw",
                Description = "This 10 in. jobsite table saw is powered by a 15 Amp motor with a 32-1/2 in. rip capacity that lets you cut larger shelving, trim boards, and hardwoods w",
                Image = "https://www.dewalt.com/NA/product/images/3000x3000x96/DWE7491RS/DWE7491RS_2.jpg",
                Price= "$20",
                QuantityAvailable= 1,
                Category= "Home Improvement"
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
            Assert.AreEqual(true, pageModel.FormResult.Contains("Successfully"));
        }
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

        /// <summary>
        /// Tests an invalid product model
        /// </summary>
        [Test]
        public void OnPostAsync_InValid_ProductId_NotValid_Return_Page()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                Id = "3TEST",//invalid id
                ToolName = "Table Saw",
                Description = "This 10 in. jobsite table saw is powered by a 15 Amp motor with a 32-1/2 in. rip capacity that lets you cut larger shelving, trim boards, and hardwoods w",
                Image = "https://www.dewalt.com/NA/product/images/3000x3000x96/DWE7491RS/DWE7491RS_2.jpg",
                Price = "$20",
                QuantityAvailable = 1,
                Category = "Home Improvement"
            };
            string test= "Can't update the tool, please provide valid information and try again.";

            // Force an invalid error state
           // pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(test, pageModel.FormResult);
        }
        #endregion OnPostAsync
    }
}