using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.Rent
{
    /// <summary>
    /// Created a class to test Rent.cshtml.cs
    /// </summary>
    public class RentTests
    {
        #region TestSetup
        public static RentModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new RentModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup
        /// <summary>
        /// Test HTTP GET to return valid product
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
        /// <summary>
        /// Test HTTP GET for invalid product id
        /// </summary>

        
        [Test]
        public void OnGet_InValid_Should_Return_ToolPage()
        {
            // Arrange

            // Act
            var result=pageModel.OnGet("test") as RedirectToPageResult;

            // Assert
            
            Assert.AreEqual(true, result.PageName.Contains("ToolPage"));
        }
        #endregion OnGet

        #region OnPostAsync
        /// <summary>
        /// this class test HTTP POST should return valid product,test redirect page and FormResult
        /// </summary>
        [Test]
        
       
        public void OnPostAsync_Valid_Should_Return_Products()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                Id = "1",
            };
            pageModel.RentalInfo = new Rental
            {
                FirstName = "Prachitee",
                LastName= "Chouhan",
                Email="prachitee.c@gmail.com",
                PhoneNumber="555-555-5555",
                RentalDate="01/01/2022"

            };
            


            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

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
            string test = "Can't process your renting, please provide valid information and try again.";

            pageModel.RentalInfo = new Rental
            {
                FirstName = "bogus",
                LastName = "bogus",
                Email = "bogus"
            };

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            
            Assert.AreEqual(test, pageModel.FormResult);
        }
        #endregion OnPostAsync


    }
}