using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _016_AngularJs_Mvc_Demo.Models
{
    public class UserModels
    {


        public int UserID { get; set; }

        [Required(ErrorMessage = "First Name is required ")]
        [RegularExpression("[a-zA-Z]{3,30}", ErrorMessage = "Enter valid FirstName ")]
        public string FName { get; set; }

        [RegularExpression("[a-zA-Z]{3,30}", ErrorMessage = "Enter valid LastName ")]
        public string LName { get; set; }

        [Required(ErrorMessage = "User Name is required ")]
        [RegularExpression(@"^[a-z0-9_]{5,15}$", ErrorMessage = " Enter valid UserName ")]
        public string UName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression("[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})", ErrorMessage = "Enter valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter valid Mobile Number")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z]).{8,}$", ErrorMessage = "Password should contains at least 8 character having one Upper case letter and one digit")]
        public string Password { get; set; }

        [CompareAttribute("Password", ErrorMessage = "Mismatch Password")]
        public string ConfirmPassword { get; set; }

        public string ImageName { get; set; }

        public string SearchString { get; set; }

    }
}