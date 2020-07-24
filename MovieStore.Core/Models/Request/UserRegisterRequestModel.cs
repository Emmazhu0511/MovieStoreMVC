using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieStore.Core.Models.Request
{
    //Model class is present for UI properties you want to store in the DB, becasue ex, User object has
    //a lot of properties, but when we create a user, we don't need all info
    public class UserRegisterRequestModel
    {
        //Data Annotations are useful for validation in ASP.NET Core/Framework
        [Required]
        [EmailAddress]   //Type is EmailAddress, can used for email format check
        [StringLength(50)]
        public string Email { get; set; }


        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }


        [Required]
        [StringLength(50)]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Make Sure password length is between 8 and 20", MinimumLength = 8)]
        //Password should be strong, One Upper letter, lower letter, a number and a special character
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        public string Password { get; set; }
    }
}

//create view from account controller and add template as create becasue we want to create a user
//Add model, then the C# model class code, like the annotation will help to generate the HTML page for us