using System.ComponentModel.DataAnnotations;

namespace SampleApplication.Web.Models
{
    public class SampleModel
    {
        [Required]
        public string Name { get; set; }
    }
}