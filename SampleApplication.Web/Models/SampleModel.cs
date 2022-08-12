using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApplication.Web.Models
{
    public class SampleModel
    {
        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required, Display(Name = "Date of Birth (string)")]
        public string DateOfBirthString { get; set; }

        [Required, Display(Name = "File")]
        public string File { get; set; }

        // Some data types are typically not nullable. However, we can add "?" after the
        // property type to establish it is nullable.
        // This will reflect in the front-end validation as well.
        [Display(Name = "Date of Birth (DateTime)")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Option")]
        public bool Option { get; set; }

        // Some data types are typically not nullable. However, we can add "?" after the
        // property type to establish it is nullable.
        // This will reflect in the front-end validation as well.
        [DataType(DataType.Currency), Display(Name = "What salary would you like to make this year?")]
        public double Salary { get; set; }

        [Display(Name = "Enable super cool feature?")]
        public bool SuperCoolFeature { get; set; }
    }
}