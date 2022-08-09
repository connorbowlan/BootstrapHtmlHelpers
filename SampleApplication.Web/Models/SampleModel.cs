using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApplication.Web.Models
{
    public class SampleModel
    {
        [Required, Display(Name = "What is your name?")]
        public string Name { get; set; }

        [Required, Display(Name = "What is your email address?")]
        public string Email { get; set; }

        [Required, Display(Name = "What is your phone number?"), MinLength(10), MaxLength(10)]
        public string PhoneNumber { get; set; }

        [Required, Display(Name = "What is your birthday?")]
        public DateTime Birthday { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = "What salary would you like to make this year?")]
        public double Salary { get; set; }

        [Required, Display(Name = "What is your favorite color?")]
        public string Color { get; set; }
    }
}