using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System.Linq;
namespace UnitTests.Pages.Product
{
    /// <summary>
    /// This  class test JsonFileProductService.cs
    /// </summary>
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region AddRating
        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }     

        [Test]
        public void AddRating_InValid_Product_Should_Return_False()
        {
            // Arrange
            //act
            var result = TestHelper.ProductService.AddRating("95", 1);

            //assert
            Assert.AreEqual(false, result);

        }

        [Test]
        public void AddRating_Product_Not_Found_Return_False()
        {
            // Arrange
            //act
            var result = TestHelper.ProductService.AddRating("95", 1);

            //assert
            Assert.AreEqual(false, result);

        }

        [Test]
        public void AddRating_Invalid_Rating_High()
        {
            // Arrange
            var data = TestHelper.ProductService.GetAllData().First();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Ratings_Empty_Return_True()
        {
            // Arrange

            // Get the last data item
            var data = TestHelper.ProductService.GetAllData().Last();
            int countOriginal = 0;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ProductService.GetAllData().Last();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());


        }
        [Test]
        public void AddRating_Invalid_Rating_Low()
        {
            // Arrange
            var data = TestHelper.ProductService.GetAllData().First();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, -1);

            // Assert
            Assert.AreEqual(false, result);
        }

        #endregion AddRating
        #region AddUserInfoRent

        /// <summary>
        /// proudct id is null
        /// </summary>
        [Test]
        public void AddUserInfoRent_Null_Product()
        {
            //arrange
            ProductModel testProduct = new ProductModel();
            Rental testRentalInfo = new Rental(){ FirstName = "David",
                LastName = "Nguyen",
                Email = "nguyendavid@seattleu.edu",
                PhoneNumber = "222-222-2222",
                RentalDate = "05/10/2022"
            };

            //act
            var result = TestHelper.ProductService.AddUserInfoRent(null, testRentalInfo);
            //var testStatus = TestHelper.ProductService.AddUserInfoRent().status;

            //assert
            Assert.AreEqual(null, result); 
        
        }
       
        /// <summary>
        /// this test when product id is invalid and not found
        /// </summary>

        [Test]
        public void AddUserInfoRent_Invalid_Product_Should_Status_False()
        {
            //arrange

            Rental testRentalInfo = new Rental()
            {
                FirstName = "David",
                LastName = "Nguyen",
                Email = "nguyendavid@seattleu.edu",
                PhoneNumber = "222-222-2222",
                RentalDate = "05/10/2022"
            };
            //act
            var result = TestHelper.ProductService.AddUserInfoRent("106",testRentalInfo);

            //assert
            Assert.AreEqual(null, result);

        }
        

        [Test]
        /// <summary>
        /// this unit test checks when product id is valid and Rental
        /// information is added and quantity avaiable is decreased by one
        /// </summary>
        public void AddUserInfoRent_Valid_Product()
        {
            //arrange
            ProductModel testProduct = new ProductModel();
            Rental testRentalInfo = new Rental()
            {
                FirstName = "david",
                LastName = "nguyen",
                Email = "nguyendavid@seattleu.edu",
                PhoneNumber = "222-222-2222",
                RentalDate = "05/10/2022"
            };
            var data = TestHelper.ProductService.GetAllData().Last();
            
            var quantityTest = data.QuantityAvailable;
            //act
            var result = TestHelper.ProductService.AddUserInfoRent(data.Id, testRentalInfo);
            var resultTest = result.Rentals.Last();
            var quantityTestAfter = result.QuantityAvailable;

            //assert
            //test if QuantityAvaiable is reduce by 1
            Assert.AreEqual(quantityTest -1, quantityTestAfter);

            //check if testRentalInfo is added in Rentals information
            Assert.AreEqual(resultTest, testRentalInfo);

        }
        #endregion AddUserInfoRent

        #region RemoveUserInfoReturn
        /// <summary>
        /// this test when user product information is invalid
        /// </summary>
        [Test]
        public void RemoveUserInfoReturn_Invalid_ProductId__Should_Return_Null()
        {
            //arrange
            string firstname = "bogus";
            string lastname = "bogus";
            string email = "bogus";

            //act
            var result = TestHelper.ProductService.RemoveUserInfoReturn(null, firstname, lastname, email);
            //assert
            Assert.AreEqual(result, null);
        }
        /// <summary>
        /// this test when user info is invalid
        /// </summary>
        [Test]
        public void RemoveUserInfoReturn_Invalid_User_Info_Should_Return_Null()
        {
            //arrange
            string firstname = "bogus";
            string lastname = "bogus";
            string email = "bogus";
            var data = TestHelper.ProductService.GetAllData().First();

            //act
            var result = TestHelper.ProductService.RemoveUserInfoReturn(data.Id, firstname, lastname, email);
            //assert
            Assert.AreEqual(result, null);
        }
        /// <summary>
        /// this test when user info is valid
        /// </summary>
        [Test]
        public void RemoveUserInfoReturn_Valid_User_Info_Should_Return_Tool()
        {
            //arrange
           
            ProductModel testProduct = new ProductModel();
            Rental testRentalInfo = new Rental()
            {
                FirstName = "david",
                LastName = "nguyen",
                Email = "nguyendavid@seattleu.edu",
                PhoneNumber = "222-222-2222",
                RentalDate = "05/10/2022"
            };
            var data = TestHelper.ProductService.GetAllData().Last();
            var result = TestHelper.ProductService.AddUserInfoRent(data.Id, testRentalInfo);
            //var quantityBeforeRemove = data.QuantityAvailable;
            string firstname = "david";
            string lastname = "nguyen";
            string email = "nguyendavid@seattleu.edu";
            
            //act
           
            var removeResult = TestHelper.ProductService.RemoveUserInfoReturn(data.Id, firstname, lastname, email);
            
            //assert  
            Assert.AreEqual(data.QuantityAvailable,TestHelper.ProductService.GetAllData().Last().QuantityAvailable);
            Assert.AreEqual(data.QuantityAvailable, removeResult.QuantityAvailable);

            Assert.AreEqual(data.Rentals.Count(),TestHelper.ProductService.GetAllData().Last().Rentals.Count());
            
        }
        #endregion RemoveUserInfoReturn
    }
}