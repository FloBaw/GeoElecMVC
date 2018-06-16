using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GeoElecMVC.Models
{
    public class Lessee
    {
        [Key]
        public int Lessee_id { get; set; }
        //public string Client_id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string First_name { get; set; }

        public string Address { get; set; }

        public string Postcode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Phone_number { get; set; }

        public string Email { get; set; }

        public string Fax { get; set; }

    }
}
