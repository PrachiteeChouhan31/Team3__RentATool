using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// This class holds data for a tool rental
    /// </summary>
    public class Rental
    {
        //this variable holds order number as an Id
        public string OrderNumber { get; set; }
        //this variable holds first name of the user, strimg can have number,atleast one alphabet and specia; symbol ,.-'
        [Required(ErrorMessage = "This First Name is required *")]
        [StringLength(20)]
        [RegularExpression(@"^(?=.*[a-zA-Z])[a-zA-Z0-9,'-.\s]*$", ErrorMessage = "Invalid First Name format.It can be alphanumberic starting with alphabets and can contain space and special characters ,' -.")]
        public string FirstName { get; set; }
        //this variable holds the last name,strimg can have number,atleast one alphabet and specia; symbol ,.-'
        [Required(ErrorMessage = "This Last Name is required *")]
        [StringLength(20)]
        [RegularExpression(@"^(?=.*[a-zA-Z])[\w,'-.\s]*$", ErrorMessage = "Invalid Last Name format.It can be alphanumberic starting with alphabets and can contain space and special characters ,' -.")]

        public string LastName { get; set; }
        //this variable holds email
        [Required(ErrorMessage ="This email address is required *")]
        public string Email { get; set; }
        //this variable hold phone number
        [Required(ErrorMessage = " Phone Number is required *")]
        [StringLength(15)]
        [RegularExpression(@"^([1-9]\d{2})?[\-](\d{3})?[\-](\d{4})?$", ErrorMessage = "Invalid Phone number format.")]
        public string PhoneNumber { get; set; }
        //this variable holds Rental date
        [Required(ErrorMessage = " Rental Date is required *")]
        [RegularExpression (@"^(1[0-2]|0[1-9])/(3[01]|[12][0-9]|0[1-9])/[0-9]{4}$", ErrorMessage ="Invalid Date format.")]
        public string RentalDate { get; set; }
        public override string ToString() => JsonSerializer.Serialize<Rental>(this);
    }
}