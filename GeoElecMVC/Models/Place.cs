using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GeoElecMVC.Models
{
    public class Place
    {
        [Key]
        public int Place_id { get; set; }
        //public string Client_id { get; set; }

        [Required]
        public string Address { get; set; }

        public string Postcode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
