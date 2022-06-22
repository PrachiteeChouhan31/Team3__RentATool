using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ContosoCrafts.WebSite.Services
{/// <summary>
 /// This class is use to add a service and to make the json reusable 
 /// </summary>

    public class JsonFileProductService
    {
        /// <summary>
        /// this is a constructor 
        /// </summary>
        
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
           
        }

        /// <summary>
        /// this method will keep the webHostEnvironment for retrive json file
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Thius method gives json file location ;path
        /// </summary>
       
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        /// <summary>
        /// As json file is located, this method convert the json object into list of products(for each)
        /// </summary>
  
        public IEnumerable<ProductModel> GetAllData()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
        /// <summary>
        /// This class add the user information who has rented the tool
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rentalInfo"></param>

        public ProductModel AddUserInfoRent(string productId, Rental rentalInfo)
        {
           
            // If the ProductID is invalid, return null
            if (string.IsNullOrEmpty(productId))
            {
                return null;
            }
            var products = GetAllData();
            // Look up the product, if it does not exist, return null
            var lookupData = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (lookupData == null)
            {
                return null;
            }
            
            rentalInfo.FirstName = rentalInfo.FirstName.ToLower();
            rentalInfo.LastName = rentalInfo.LastName.ToLower();
            rentalInfo.Email = rentalInfo.Email.ToLower();
            //get the product that user want to rent
            var data = products.First(x => x.Id == productId);
            //assigning unique order number
            rentalInfo.OrderNumber = System.Guid.NewGuid().ToString();

            //append user information
            data.Rentals = data.Rentals.Append(rentalInfo);
            //decreasing the quantity by 1
            data.QuantityAvailable --;

            SaveData(products);
           
            return data;


        }
        /// <summary>
        /// This method remove the user information from the Rentals and increase the quanity available by 1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public ProductModel RemoveUserInfoReturn(string id, string firstName, string lastName, string email)
        {
            // If the ProductID is invalid, return null
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            //get all data
            var dataSet = GetAllData();
            //get the data that matches id
            var deleteFromTool = dataSet.FirstOrDefault(m => m.Id.Equals(id));
            //get user information to extract OrderNumber
            
            if (deleteFromTool == null)
            {
                return null;
            }
            
            firstName = firstName.ToLower();
            lastName = lastName.ToLower();
            email = email.ToLower();
            var deleteUserInfo = deleteFromTool.Rentals.FirstOrDefault(m => m.FirstName.Equals(firstName) & m.LastName.Equals(lastName) & m.Email.Equals(email));
            if (deleteUserInfo == null)
            {
                return null;
            }

            //remove the user by selcting other users
            deleteFromTool.Rentals = from user in deleteFromTool.Rentals
                                     where user.OrderNumber != deleteUserInfo.OrderNumber
                                     select user;
            deleteFromTool.QuantityAvailable += 1;
            SaveData(dataSet);
            //return the tool that is returned removing the user info
            return deleteFromTool;

        }


        /// <summary>
        /// this method add rating to json file
        /// </summary>

        public bool AddRating(string productId, int rating)
        {
            // If the ProductID is invalid, return
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            var products = GetAllData();

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (data == null)
            {
                return false;
            }


            // Check Rating for boundries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if ratings exist, if there are not, then create the array
            if (data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveData(products);

            return true;

        }

        /// <summary>
        /// Save All products data to storage
        /// </summary>
        private void SaveData(IEnumerable<ProductModel> products)
        {

            using (var outputStream = File.Create(JsonFileName))
            {
                //serialize the data to store in products.json
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }
        /// <summary>
        /// Create a new product using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns></returns>
        public ProductModel CreateData()
        {
            var data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                ToolName = "Enter Tool Name",
                Description = "Enter Description",
                Image = "Enter image",
                QuantityAvailable=0,
                Price="Enter Price",
                Ratings = null,
               
            };

            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            dataSet = dataSet.Append(data);

            SaveData(dataSet);

            return data;
        }

        /// <summary>
        /// Create a new product using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns></returns>
        public ProductModel CreateData(ProductModel data)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            dataSet = dataSet.Append(data);

            SaveData(dataSet);

            return data;
        }

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns></returns>
        public ProductModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);

            SaveData(newDataSet);

            return data;
        }
               /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name="data"></param>
        public ProductModel UpdateData(ProductModel data)
        {
            var products = GetAllData();
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (productData == null)
            {
                return null;
            }
           
            productData.ToolName = data.ToolName;
            productData.Description = data.Description;
            productData.Image = data.Image;
            productData.Id = data.Id;
            productData.Price = data.Price;
            productData.QuantityAvailable = data.QuantityAvailable;
            productData.Category = data.Category;
            productData.CommentList = data.CommentList;

            SaveData(products);

            return productData;
        }
        
    }
}