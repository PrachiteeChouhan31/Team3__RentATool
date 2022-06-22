using ContosoCrafts.WebSite.Controllers;
using NUnit.Framework;
using static ContosoCrafts.WebSite.Controllers.ProductsController;

namespace UnitTests.Controllers
{
	/// <summary>
	/// Class to test the controller
	/// </summary>
    public class Product
	{

		#region TestSetup
		private static ProductsController productsController;
		private static RatingRequest request;

		/// <summary>
		/// Initializes ProductsController
		/// </summary>
		[SetUp]
		public void TestInitialize()
		{
			productsController = new ProductsController(TestHelper.ProductService);
		}
		#endregion TestSetup

		/// <summary>
		/// Product controller test to test Get Method;
		/// </summary>
		[Test]
		public void Get_method()
		{
			// Arrange & Act
			var result = productsController.Get();

			// Reset

			// Assert
			Assert.IsNotEmpty(result);
		}

		/// <summary>
		/// Product controller test to test Patch method
		/// </summary>
		[Test]
		public void Patch_method()
		{
			// Arrange
			request = new RatingRequest
			{
				ProductId = "99999999999999999999999999",
				//test rating record feature
				Rating = 5
			};
			// Act
			var result = productsController.Patch(request);

			// Reset

			// Assert
			Assert.That(result, Is.Not.Null);
		}
	}
}
