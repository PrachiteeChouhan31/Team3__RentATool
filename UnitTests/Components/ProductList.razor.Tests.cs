using Bunit;
using NUnit.Framework;
using ContosoCrafts.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using ContosoCrafts.WebSite.Services;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Moq;
using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;

namespace UnitTests.Components
{
    /// <summary>
    /// Class to test the ProductList razor component
    /// </summary>
    public class ProductListTests : BunitTestContext
    {
        /// <summary>
        /// Test set up
        /// </summary>
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        /// <summary>
        /// Tests to see if page markup contains the name of at least one tool 
        /// </summary>
        [Test]
        public void ProductList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("25 Ft Measuring Tape"));
        }

        /// <summary>
        /// Test to see if clicking the description button for a tool shows its description in the page markeup.
        /// </summary>
        #region SelectProduct
        [Test]
        public void SelectProduct_Valid_ID_3_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_3";

            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("These measuring tapes deliver up to 12 ft. of reach."));
        }
        #endregion SelectProduct

        /// <summary>
        /// Tests to verify that clicking on a star for a tool that has no votes will change the number of votes for the tool and the overall rating
        /// </summary>
        #region SubmitRating

        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            /*
             This test tests that the SubmitRating will change the vote as well as the Star checked
             Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed

            The test needs to open the page
            Then open the popup on the card
            Then record the state of the count and star check status
            Then check a star
            Then check again the state of the count and star check status

            */

            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_1";

            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Need to get the count of cards to find the vote string to account for all of the rent and return buttons that are within "spans"
            var divList = page.FindAll("div");
            var cardCount = divList.Where(m => (m.ClassName != null) && m.ClassName.Equals("card")).Count();

            // Get the Vote Count
            // Get the Vote Count, the star button list holds the rent and return buttons, so the index of the first star button string must take them into account 
            var preVoteCountSpan = starButtonList[(cardCount*2) - 1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the First star item from the list, it should not be checked
            var starButton = starButtonList.First(m=>!string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the star button list holds the rent and return buttons, so the index of the first star button string must take them into account 
            var postVoteCountSpan = starButtonList[(cardCount * 2) - 1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("Be the first to vote!"));
            Assert.AreEqual(true, postVoteCountString.Contains("1 Vote"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }

        /// <summary>
        /// This test verifies that clicking a star for a tool that already has votes will update its vote count and overall rating
        /// </summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Stared_Should_Increment_Count_And_Leave_Star_Check_Remaining()
        {
            /*
             This test tests that the SubmitRating will change the vote as well as the Star checked
             Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed

            The test needs to open the page
            Then open the popup on the card
            Then record the state of the count and star check status
            Then check a star
            Then check again the state of the cound and star check status

            */

            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_2";

            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Get the Vote Count, the star button list holds the rent and return buttons, so the index of the first star button string must take them into account 
            var divList = page.FindAll("div");
            var cardCount = divList.Where(m => (m.ClassName != null) && m.ClassName.Equals("card")).Count() - 1;

            // Get the Vote Count
            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var preVoteCountSpan = starButtonList[(cardCount * 2) + 1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the Last star item from the list, it should one that is checked
            var starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the star button list holds the rent and return buttons, so the index of the first star button string must take them into account 
            var postVoteCountSpan = starButtonList[(cardCount*2)+1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had 1 vote to start, and 2 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("1 Vote"));
            Assert.AreEqual(true, postVoteCountString.Contains("2 Votes"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }
        #endregion SubmitRating



        #region FilterData
        /// <summary>
        /// This test verifies that entering a string in the textbox and clicking the filter button will filter the displayed inventory to tools with names that contain the input string.
        /// </summary>
        [Test]
        public void FilterData_Valid_Click_Filter_Should_Filter_Displayed_Tools_By_Input_String()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var filterButtonID = "FilterButton";
            var filterInputID = "FilterInput";

            // Get the markup before filtering
            var page = RenderComponent<ProductList>();
            var preFilterResult = page.Markup;

            // Find the filter button and filter input
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("Input");
            var filterInput = inputList.First(m => m.OuterHtml.Contains(filterInputID));
            var filterButton = buttonList.First(m => m.OuterHtml.Contains(filterButtonID));

            // Act
            // Change the filter input text and click the filter button
            filterInput.Change("saw");           
            filterButton.Click();

            // Get the markup after filtering
            var postFilterResult = page.Markup;

            // Assert
            Assert.AreEqual(true, preFilterResult.Contains("Hammer"));
            Assert.AreEqual(true, postFilterResult.Contains("Table Saw"));
            Assert.AreEqual(false, postFilterResult.Contains("Hammer"));
        }

        /// <summary>
        /// This test verifies that after the inventory is filtered, clicking the clear button will restore the display to show all tools in the inventoy
        /// </summary>
        [Test]
        public void ClearFilterData_Valid_Should_Restore_Display_To_All_Tools()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var filterButtonID = "FilterButton";
            var clearButtonID = "ClearButton";
            var filterInputID = "FilterInput";

            // Get the markup before filtering
            var page = RenderComponent<ProductList>();
            var preFilterResult = page.Markup;

            // Find the filter and clear buttons and filter input
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("Input");
            var filterInput = inputList.First(m => m.OuterHtml.Contains(filterInputID));
            var filterButton = buttonList.First(m => m.OuterHtml.Contains(filterButtonID));
            var clearButton = buttonList.First(m => m.OuterHtml.Contains(clearButtonID));

            // Change the filter input text and click the filter button so the tools are filtered
            filterInput.Change("saw");
            filterButton.Click();

            // Get the markup after filtering
            var postFilterResult = page.Markup;

            // Act
            clearButton.Click();

            // Get the markup after clearing the filter
            var postClearFilterResult = page.Markup;

            // Assert
            Assert.AreEqual(true, preFilterResult.Contains("Hammer"));
            Assert.AreEqual(true, postFilterResult.Contains("Table Saw"));
            Assert.AreEqual(false, postFilterResult.Contains("Hammer"));
            Assert.AreEqual(true, postClearFilterResult.Contains("Table Saw"));
            Assert.AreEqual(true, postClearFilterResult.Contains("Hammer"));
        }

        /// <summary>
        /// This test verifies that if filtering has no matches, a message will be displayed saying so
        /// </summary>
        [Test]
        public void FilterData_Valid_Click_Filter_With_No_Matches_Should_Display_Message()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var filterButtonID = "FilterButton";
            var filterInputID = "FilterInput";

            // Get the markup before filtering
            var page = RenderComponent<ProductList>();
            var preFilterResult = page.Markup;

            // Find the filter button and filter input
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("Input");
            var filterInput = inputList.First(m => m.OuterHtml.Contains(filterInputID));
            var filterButton = buttonList.First(m => m.OuterHtml.Contains(filterButtonID));

            // Act
            // Change the filter input text and click the filter button
            filterInput.Change("ABCD12345");
            filterButton.Click();

            // Get the markup after filtering
            var postFilterResult = page.Markup;

            // Assert
            Assert.AreEqual(true, preFilterResult.Contains("Hammer"));
            Assert.AreEqual(true, preFilterResult.Contains("Table Saw"));
            Assert.AreEqual(true, postFilterResult.Contains("There are no matches for the filter term you entered and/or the category you chose."));
        }

        /// <summary>
        /// This test verifies that after the inventory is filtered, clicking the clear button will restore the display to show all tools in the inventoy
        /// </summary>
        [Test]
        public void ClearFilterData_Valid_After_No_Matches_Should_Restore_Display_To_All_Tools()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var filterButtonID = "FilterButton";
            var clearButtonID = "ClearButton";
            var filterInputID = "FilterInput";

            // Get the markup before filtering
            var page = RenderComponent<ProductList>();

            // Find the filter and clear buttons and filter input
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("Input");
            var filterInput = inputList.First(m => m.OuterHtml.Contains(filterInputID));
            var filterButton = buttonList.First(m => m.OuterHtml.Contains(filterButtonID));
            var clearButton = buttonList.First(m => m.OuterHtml.Contains(clearButtonID));

            // Change the filter input text and click the filter button so the tools are filtered
            filterInput.Change("ABCD12345");
            filterButton.Click();

            // Get the markup after filtering
            var preClearFilterResult = page.Markup;

            // Act
            clearButton.Click();

            // Get the markup after clearing the filter
            var postClearFilterResult = page.Markup;

            // Assert            
            Assert.AreEqual(true, preClearFilterResult.Contains("no matches for the filter term you entered"));
            Assert.AreEqual(true, postClearFilterResult.Contains("Table Saw"));
            Assert.AreEqual(true, postClearFilterResult.Contains("Hammer"));
        }
        #endregion FilterData

        /// <summary>
        /// Tests to verify that correct button is click and on clicking the rent button navigates to the rent page for that tool
        /// </summary>
        [Test]
        public void MoveToRent_Should_Navigate_To_Rent_Page()
        {
            // Arrange
            //rendering a component happens thru bunit's testContest. so var ctc create a new instance of disposable BUnit TestContext. 
            var ctc = new Bunit.TestContext();
            ctc.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var id = "RentButton_6";
            //perform rendering base on test context
            var page = ctc.RenderComponent<ProductList>();

            var preRentClickResult = page.Markup;

            // Find the Buttons (Rent for product id 6)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            //click the button 
            button.Click();
            //after button click navigate to rent page with product id 6
            //get blazor's  fake navigation manager and added by default bunit's TestContext.Services
            var nav = ctc.Services.GetRequiredService<NavigationManager>();
            //get current page uri
            var currentURI = nav.Uri;
            // Get the Cards retrned


            // Assert
            Assert.AreEqual("RentButton_6",button.Id);//checks whether correct button is click
            Assert.AreEqual("http://localhost/Product/Rent/6", currentURI);//checks whether redirected to correct page after Rent button click
           
        }


        /// <summary>
        /// Tests to verify that correct button is click and on clicking the return button navigates to the return page for that tool
        /// </summary>
        [Test]
        public void MoveToReturn_Should_Navigate_To_Return_Page()
        {
            // Arrange
            //rendering a component happens thru bunit's testContest. so var ctc create a new instance of disposable BUnit TestContext. 
            var ctc = new Bunit.TestContext();
            ctc.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var id = "ReturnButton_6";

            var page = ctc.RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();
            //after button click navigate to return page with product id 6
            //get blazor's  fake navigation manager and added by default bunit's TestContext.Services
            var nav = ctc.Services.GetRequiredService<NavigationManager>();
            //get current page uri
            var currentURI = nav.Uri;


            // Assert
            Assert.AreEqual("ReturnButton_6", button.Id);//checks whether correct button is click
            Assert.AreEqual("http://localhost/Product/Return/6", currentURI);//checks whether redirected to correct page after Return button click

        }

        /// <summary>
        /// Test method to test add comment method and check if the comments exist 
        /// in the product's detail
        /// </summary>
        [Test]
        public void FormResult_Should_Able_To_Add_Comment_To_A_Product()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net5.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");
            var MockLoggerDirect = Mock.Of<ILogger<ToolPageModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);
            var selectedProduct = productService.GetAllData().First(x => x.Id == "1");

            var newComment_1 = new CommentModel
            {
                Comment = "this is a very nice product to use"
            };

            var newComment_2 = new CommentModel
            {
                Comment = "this product is easy to learn"
            };

            List<string> commentList = new List<string>();

            // Act
            selectedProduct.CommentList.Add(newComment_1);
            selectedProduct.CommentList.Add(newComment_2);
            foreach (var comment in selectedProduct.CommentList)
            {
                commentList.Add(comment.Comment);
            }

            // Assert
            bool commentAdded = selectedProduct.CommentList.Contains(newComment_1);
            Assert.IsTrue(commentAdded);

            bool specificComment = commentList.Contains(newComment_1.Comment);
            Assert.AreEqual(true, specificComment);
        }

        /// <summary>
        /// Test method to check if the comment added to the product
        /// </summary>
        [Test]
        public void Add_Comment_To_A_Product_Comment_Should_Be_Added()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Get the markup before filtering
            var page = RenderComponent<ProductList>();

            var id = "MoreInfoButton_8";
            var commentId = "AddComment";
            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();            

            // Find the comment button
            var buttonList_2 = page.FindAll("Button");
            var commentButton = buttonList_2.First(m => m.OuterHtml.Contains(commentId));

            // Act
            commentButton.Click();
            string comment_1 = "this product is easy to learn";
       

            var inputList = page.FindAll("Input");
            var commentInput = inputList.First(m => m.OuterHtml.Contains("commentInput"));

            // Find the save and clear buttons and click them
            commentInput.Change(comment_1);
            var buttons = page.FindAll("Button");
            var saveButton = buttons.First(m => m.OuterHtml.Contains("saveComment"));
            var clearButton = buttons.First(m => m.OuterHtml.Contains("clearComment"));            
            clearButton.Click();
            var buttonList_3 = page.FindAll("Button");
            var commentButton_3 = buttonList_3.First(m => m.OuterHtml.Contains(commentId));
            commentButton_3.Click();

            // Repeat the add comment and click the save button
            string comment_2 = "this product is easy to learn";
            var inputList_2 = page.FindAll("Input");
            var commentInput_2 = inputList_2.First(m => m.OuterHtml.Contains("commentInput"));
            commentInput_2.Change(comment_2);
            var buttons_2 = page.FindAll("Button");
            var saveButton_2 = buttons_2.First(m => m.OuterHtml.Contains("saveComment"));
            commentInput_2.Change(comment_2);
            saveButton_2.Click();

            // Get the markup after filtering
            var postCommentadded = page.Markup;

            bool commentAdded_1 = postCommentadded.Contains(comment_2);

            // Assert
            Assert.AreEqual(true, commentAdded_1);
        }

        /// <summary>
        /// Test to see if selecting automotive care in the dropdown menu filters the inventory appropriately
        /// </summary>
        [Test]
        public void Select_Category_Automotive_Valid_Should_Filter_Inventory_By_Automotive()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Get the markup before filtering
            var page = RenderComponent<ProductList>();
            var preFilterResult = page.Markup;

            // Find the dropdown menu 
            var categoryID = "CategoryBox";
            var dropdownList = page.FindAll("Select");

            // Find the one that matches the ID looking for
            var dropdownMenu = dropdownList.First(m => m.OuterHtml.Contains(categoryID));

            // Act
            dropdownMenu.Change("Automotive Care");
            // Get the markup afte filtering
            var postFilterResult = page.Markup;

            // Assert
            Assert.AreEqual(true, preFilterResult.Contains("Wrench Set"));
            Assert.AreEqual(true, postFilterResult.Contains("Wrench Set"));
            Assert.AreEqual(false, postFilterResult.Contains("Hammer"));
        }

        /// <summary>
        /// Test to see if selecting automotive care in the dropdown menu filters the inventory appropriately
        /// </summary>
        [Test]
        public void Select_Category_Select_A_Category_Valid_Should_Restore_Entire_Inventory()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Get the markup before filtering
            var page = RenderComponent<ProductList>();
            var preFilterResult = page.Markup;

            // Find the dropdown menu 
            var categoryID = "CategoryBox";
            var dropdownList = page.FindAll("Select");

            // Find the one that matches the ID looking for
            var dropdownMenu = dropdownList.First(m => m.OuterHtml.Contains(categoryID));

            // Act
            dropdownMenu.Change("Automotive Care");
            dropdownMenu.Change("Select a Category");

            // Get the markup afte filtering
            var postFilterResult = page.Markup;

            // Assert
            Assert.AreEqual(true, postFilterResult.Contains("Wrench Set"));
            Assert.AreEqual(true, postFilterResult.Contains("Hammer"));
        }
    }   
}