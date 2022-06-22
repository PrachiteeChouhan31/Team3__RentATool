using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// class to build list of categories for search filter feature. 
    /// </summary>
    public class CategoriesList
    {
        //list of categoris object (included category names and ID)
        public List<Category> categories = new List<Category>();

        //home improvment catergory object
        Category homeImprovement = new Category
        {
            Id = 1,
            Name = "Home Improvement"
        };

        //home automotive catergory object
        Category automative = new Category
        {
            Id = 2,
            Name = "Automotive Care"
        };

        //home lawncare catergory object
        Category lawnCare = new Category
        {
            Id = 3,
            Name = "Lawn Care"
        };

        //home outdoor catergory object
        Category outDoor = new Category
        {
            Id = 4,
            Name = "Outdoor"
        };

        //selected list for categories (used for dropdown menu on the page)
        public SelectList RoleSelectList()
        {
            categories.Add(homeImprovement);
            categories.Add(automative);
            categories.Add(lawnCare);
            categories.Add(outDoor);
            SelectList list = new SelectList(categories, "Id", "Name");
            return list;
        }
    }
}