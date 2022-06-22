using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// this class identify the element tag based on json file 
    /// </summary>
    public class ProductModel
    {
        // Constructor to instantiate rental list
        public ProductModel()
        {
            Rentals = new List<Rental>();
        }
        
        // Unique ID for the tool
        public string Id { get; set; }

        // Image Url for the tool
        [JsonPropertyName("img")]
        public string Image { get; set; }

        // Description of the tool
        public string Description { get; set; }

        // Array for ratings
        public int[] Ratings { get; set; }
        // Name of the tool
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Invalid tool Name format.")]
        public string ToolName { get; set; }
        // Price of renting the tool for a week
       
        [StringLength(9)]
        [RegularExpression(@"^\$([0-9]*|[0-9]+\.[0-9]+)$", ErrorMessage = "Invalid price format.")]
        public string Price { get; set; }

        // Quantity of the tool available to rent
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid format.")]
        public int QuantityAvailable { get; set; }

        // Category the tool belongs to
        public string Category { get; set; }

        // Collection of rentals for the tool
        public IEnumerable<Rental> Rentals { get; set; }

        // Collection of comments about the tool
        public List<CommentModel> CommentList { get; set; } = new List<CommentModel>();

        /// <summary>
        ///This method looks the objects as a string and covert the information as a json file identifiable text
        /// </summary>

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}