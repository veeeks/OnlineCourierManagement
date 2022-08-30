using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourierManagement.MVC.ViewModels
{
    public class AddDEViewModel
    {
        public int Delivery_Id { get; set; }

       // [Display(Name = "Email")]
        //[Required]
        //[RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Please Enter a valid e-Mail")]
        public string DE_Email { get; set; }

       // [Display(Name = "Password")]
        
       // [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Please enter a strong password")]
        public string DE_Password { get; set; }

        //[Display(Name = "Confirm Password")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Passwords don't match")]
        //public string DE_Confirm_Password { get; set; }

        [Display(Name = "Mobile Number")]
        [Required]
        [RegularExpression(@"^[6789][0-9]{9}$", ErrorMessage = "Enter correct mobile number")]
        public string Contact_number { get; set; }
        public string Name { get; set; }
        //public string DStatus { get; set; }
        [Display(Name = "Origin City")]
        public string City { get; set; }
        //public Nullable<int> MaxQty { get; set; }
    }
}