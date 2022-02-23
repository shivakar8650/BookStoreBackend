using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModel
{
    public class ResetModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]+([.#_$+-][a-zA-Z0-9]+)*[@][a-zA-Z0-9]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2})?$", ErrorMessage = "Email is not valid. Please Enter valid email")]
        public string emailID { get; set; }
        [Required]
        //// [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Password is not valid. Password Should be 8 Character contain 1 Uppercase, 1 Special character, 1 Number")]
        public string newpassword { get; set; }
    }
}
