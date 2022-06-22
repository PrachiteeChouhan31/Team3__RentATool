using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;

namespace UnitTests.Pages.Product.Index
{
    /// <summary>
    /// This class tests the index CRUDi page
    /// </summary>
    public class IndexTests
    {
        #region TestSetup
        public static PageContext pageContext;

        public static IndexModel pageModel;

        /// <summary>
        /// Initializes IndexModel
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new IndexModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup
        /// <summary>
        /// Tests that model is valid and that a list of products exists.
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("test");

            //Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, pageModel.Products.ToList().Any());
        }

        /// <summary>
        /// Tests that model is valid and that a list of products exists.
        /// and product list is sorted
        /// </summary> 
        [Test]
        public void OnGet_Valid_Should_Return_Products_WithSortProduct_QueryString()
        {
            // Arrange

            // Act
            pageModel.OnGet("byName");

            //Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, pageModel.Products.ToList().Any());
        }
        #endregion OnGet
        #region FormResult
        /// <summary>
        /// to retun form result valid
        /// </summary>
        [Test]
        public void FormResult_Should_Return_Invalid_String()
        {
            //arrange
            string testFormResult = "Can't process , please provide valid information and try again.";


            // Act
            pageModel.RedirectToPage("Index");
            pageModel.FormResult = testFormResult;

            // Assert
           
            Assert.AreEqual(true, pageModel.FormResult.Contains("Can't"));


        }

        /// <summary>
        /// to rturn form result invalid
        /// </summary>
        [Test]
        public void FormResult_Should_Return_Valid_String()
        {
            //arrange
            string testFormResult = "Successful initiated";


            // Act
            pageModel.RedirectToPage("Index");
            pageModel.FormResult = testFormResult;

            // Assert
            
            Assert.AreEqual(true, pageModel.FormResult.Contains("Successful"));

        }
        #endregion FormResult
    }
}