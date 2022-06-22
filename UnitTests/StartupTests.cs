using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace UnitTests.Pages.Startup
{
    /// <summary>
    /// Tests the startup.cs file
    /// </summary>
    public class StartupTests
    {
        #region TestSetup
        /// <summary>
        /// Initializes tests 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>
        /// Calls startup to initialize 
        /// </summary>
        public class Startup : ContosoCrafts.WebSite.Startup
        {
            public Startup(IConfiguration config) : base(config) { }
        }
        #endregion TestSetup

        #region ConfigureServices
        /// <summary>
        /// Tests that startup configure services is valid
        /// </summary>
        [Test]
        public void Startup_ConfigureServices_Valid_Defaut_Should_Pass()
        {
            // Arrange

            // Act
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            // Reset

            // Assert            
            Assert.IsNotNull(webHost);
        }
        #endregion ConfigureServices
        
        #region Configure
        /// <summary>
        /// Tests that startup configuration is valid
        /// </summary>
        [Test]
        public void Startup_Configure_Valid_Defaut_Should_Pass()
        {
            // Arrange

            // Act
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();

            // Reset

            // Assert            
            Assert.IsNotNull(webHost);
        }
        #endregion Configure
    }
}