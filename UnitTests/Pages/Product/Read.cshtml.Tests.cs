using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using Microsoft.AspNetCore.Mvc;


namespace UnitTests.Pages.Product.Read
{
    /// <summary>
    /// This class tests the read CRUDi page
    /// </summary>
    public class ReadTests
    {
        #region TestSetup
        public static ReadModel pageModel;

        /// <summary>
        /// Initializes ReadModel
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new ReadModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Tests that correct product is returned when a valid id is passed in 
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

        /// <summary>
        /// Tests that when input is incorrect product id it is  returned to Index page 
        /// </summary>
        [Test]
        public void OnGet_Invalid_ProductId_Return_To_Index()
        {
            // Arrange

            // Act
            var result = pageModel.OnGet("test") as RedirectToPageResult ;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Index"));

        }
       
        #endregion OnGet
    }
}