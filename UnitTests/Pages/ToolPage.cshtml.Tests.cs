using ContosoCrafts.WebSite.Pages;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Pages.ToolPage
{
    /// <summary>
    /// This class test the tool inventory page
    /// </summary>
    public class ToolPageTests
    {
        #region TestSetup
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static ToolPageModel pageModel;

        /// <summary>
        /// Initializes a valid testing state of the tool page
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net5.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<ToolPageModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new ToolPageModel(MockLoggerDirect, productService)
            {
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Tests that number of products on the tool page equals the 
        /// number of tools in products.json
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange
            var data = TestHelper.ProductService.GetAllData();
            int correctCount = data.Count() - 1;

            // Act
            pageModel.OnGet();
            int actualCount = pageModel.Products.ToList().Count();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(correctCount, actualCount);
        }
        #endregion OnGet

        /// <summary>
        /// Tests that ProductModel ToString method returns a string
        /// </summary>
        #region PMToString
        [Test]
        public void ProductModelToString_Valid_Should_Return_NotNull()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            //Reset

            // Assert
            Assert.IsNotNull(pageModel.Products.First().ToString());
        }
        #endregion PMToString

        /// <summary>
        /// Tests that ProductModel ToString method returns a string
        /// </summary>
        #region PMRentalToString
        [Test]
        public void ProductModelRentalToString_Valid_Should_Return_NotNull()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            //Reset

            // Assert
            Assert.IsNotNull(pageModel.Products.First().Rentals.First().ToString());
        }
        /// <summary>
        /// to retun form result valid
        /// </summary>
        [Test]
        public void FormResult_Should_Return_Invalid_String() 
        {
            //arrange
            string testFormResult = "Can't process your return, please provide valid information and try again.";


            // Act
            pageModel.RedirectToPage("ToolPage");
            pageModel.FormResult = testFormResult;

            // Assert
            //string test = "Can't process your return, please provide valid information and try again.";
            Assert.AreEqual(testFormResult, pageModel.FormResult);


        }

        /// <summary>
        /// to rturn form result invalid
        /// </summary>
        [Test]
        public void FormResult_Should_Return_Valid_String() 
        {
            //arrange
            string testFormResult = "Successful initiated the returned of";


            // Act
            pageModel.RedirectToPage("ToolPage");
            pageModel.FormResult = testFormResult;

            // Assert
            //string test = "Can't process your return, please provide valid information and try again.";
            Assert.AreEqual(true, pageModel.FormResult.Contains("Successful"));

        }

        #endregion PMRentalToString
    }
}