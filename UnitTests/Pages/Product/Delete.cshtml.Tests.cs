using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.Delete
{
    /// <summary>
    /// This class tests the delete CRUDi page
    /// </summary>
    public class DeleteTests
    {
        #region TestSetup
        public static DeleteModel pageModel;

        /// <summary>
        /// Initializes DeleteModel
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new DeleteModel(TestHelper.ProductService)
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
            pageModel.OnGet("1");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Table Saw", pageModel.Product.ToolName);
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
        /// Tests a valid deletion, checks page redirects to index page after, and
        /// confirms product is deleted
        /// </summary>
        #region OnPostAsync
        [Test]
        public void OnPostAsync_Valid_Should_Return_Products()
        {
            // Arrange

            // First Create the product to delete
            pageModel.Product = TestHelper.ProductService.CreateData();
            pageModel.Product.ToolName = "Example to Delete";
            TestHelper.ProductService.UpdateData(pageModel.Product);

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));

            // Confirm the item is deleted
            Assert.AreEqual(null, TestHelper.ProductService.GetAllData().FirstOrDefault(m=>m.Id.Equals(pageModel.Product.Id)));
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
                Id = "bogus",
                ToolName = "bogus",
                Description = "bogus",
                Image = "bougs"
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