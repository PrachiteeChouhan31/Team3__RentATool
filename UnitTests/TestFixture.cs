using NUnit.Framework;
using System.IO;

namespace UnitTests
{
    /// <summary>
    /// Class to set data and file paths for testing
    /// </summary>
    [SetUpFixture]
    public class TestFixture
    {
        // Path to the Web Root
        public static string DataWebRootPath = "./wwwroot";

        // Path to the data folder for the content
        public static string DataContentRootPath = "./data/";

        /// <summary>
        /// Method to set up data paths before tests are run
        /// </summary>
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // Run this code once when the test harness starts up.

            // This will copy over the latest version of the database files

            var DataWebPath = "../../../../src/bin/Debug/net5.0/wwwroot/data";
            var DataUTDirectory = "wwwroot";
            var DataUTPath = DataUTDirectory + "/data";

            // Delete the Detination folder
            if (Directory.Exists(DataUTDirectory))
            {
                Directory.Delete(DataUTDirectory, true);
            }
            
            // Make the directory
            Directory.CreateDirectory(DataUTPath);

            // Copy over all data files
            var filePaths = Directory.GetFiles(DataWebPath);
            foreach (var filename in filePaths)
            {
                string OriginalFilePathName = filename.ToString();
                var newFilePathName = OriginalFilePathName.Replace(DataWebPath, DataUTPath);

                File.Copy(OriginalFilePathName, newFilePathName);
            }
        }

        /// <summary>
        /// Method to run after tests are accomplished
        /// </summary>
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}