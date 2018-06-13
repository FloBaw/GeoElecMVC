using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace GeoElecMVC.Models
{
    public class ContactViewModel
    {
        [StringLength(100)]
        [Required(ErrorMessage = "The name field is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "The Email field is wrong")]
        [Required(ErrorMessage = "The Email field is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "The subject field is required")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
