using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SampleApplication.Web.Models
{
    public class SampleModel
    {
        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Name")]
        public string NameHelpText { get; set; }

        [Required, Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required, Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required, Display(Name = "Date (string type)")]
        public string DateString { get; set; }

        [Required, Display(Name = "Password")]
        [Phone]
        public string Password { get; set; }

        [Required, Display(Name = "State")]
        public string State { get; set; }

        [Required, Display(Name = "Country")]
        public Countries Country { get; set; }

        [Required, Display(Name = "File")]
        public string File { get; set; }

        // Some data types are typically not nullable. However, we can add "?" after the
        // property type to establish it is nullable.
        // This will reflect in the front-end validation as well.
        [Display(Name = "Date (DateTime type)")]
        public DateTime Date { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Option")]
        public bool CheckboxOption { get; set; }

        [Display(Name = "Option")]
        public bool RadioOption { get; set; }
    }

    public static class ListHelper
    {
        public static List<SelectListItem> States =
            new List<SelectListItem>
            {
                new SelectListItem { Value = "AL", Text = "Alabama" },
                new SelectListItem { Value = "AK", Text = "Alaska" },
                new SelectListItem { Value = "AZ", Text = "Arizona" },
                new SelectListItem { Value = "AR", Text = "Arkansas" },
                new SelectListItem { Value = "CA", Text = "California" },
                new SelectListItem { Value = "CO", Text = "Colorado" },
                new SelectListItem { Value = "CT", Text = "Connecticut" },
                new SelectListItem { Value = "DE", Text = "Delaware" },
                new SelectListItem { Value = "FL", Text = "Florida" },
                new SelectListItem { Value = "GA", Text = "Georgia" },
                new SelectListItem { Value = "HI", Text = "Hawaii" },
                new SelectListItem { Value = "ID", Text = "Idaho" },
                new SelectListItem { Value = "IL", Text = "Illinois" },
                new SelectListItem { Value = "IN", Text = "Indiana" },
                new SelectListItem { Value = "IA", Text = "Iowa" },
                new SelectListItem { Value = "KS", Text = "Kansas" },
                new SelectListItem { Value = "KY", Text = "Kentucky" },
                new SelectListItem { Value = "LA", Text = "Louisiana" },
                new SelectListItem { Value = "ME", Text = "Maine" },
                new SelectListItem { Value = "MD", Text = "Maryland" },
                new SelectListItem { Value = "MA", Text = "Massachusetts" },
                new SelectListItem { Value = "MI", Text = "Michigan" },
                new SelectListItem { Value = "MN", Text = "Minnesota" },
                new SelectListItem { Value = "MS", Text = "Mississippi" },
                new SelectListItem { Value = "MO", Text = "Missouri" },
                new SelectListItem { Value = "MT", Text = "Montana" },
                new SelectListItem { Value = "NC", Text = "North Carolina" },
                new SelectListItem { Value = "ND", Text = "North Dakota" },
                new SelectListItem { Value = "NE", Text = "Nebraska" },
                new SelectListItem { Value = "NV", Text = "Nevada" },
                new SelectListItem { Value = "NH", Text = "New Hampshire" },
                new SelectListItem { Value = "NJ", Text = "New Jersey" },
                new SelectListItem { Value = "NM", Text = "New Mexico" },
                new SelectListItem { Value = "NY", Text = "New York" },
                new SelectListItem { Value = "OH", Text = "Ohio" },
                new SelectListItem { Value = "OK", Text = "Oklahoma" },
                new SelectListItem { Value = "OR", Text = "Oregon" },
                new SelectListItem { Value = "PA", Text = "Pennsylvania" },
                new SelectListItem { Value = "RI", Text = "Rhode Island" },
                new SelectListItem { Value = "SC", Text = "South Carolina" },
                new SelectListItem { Value = "SD", Text = "South Dakota" },
                new SelectListItem { Value = "TN", Text = "Tennessee" },
                new SelectListItem { Value = "TX", Text = "Texas" },
                new SelectListItem { Value = "UT", Text = "Utah" },
                new SelectListItem { Value = "VT", Text = "Vermont" },
                new SelectListItem { Value = "VA", Text = "Virginia" },
                new SelectListItem { Value = "WA", Text = "Washington" },
                new SelectListItem { Value = "WV", Text = "West Virginia" },
                new SelectListItem { Value = "WI", Text = "Wisconsin" },
                new SelectListItem { Value = "WY", Text = "Wyoming" }
            };
    }
}