using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoElecMVC.Models
{
    public class Customer
    {
        [Key]
        public int Client_id { get; set; }
        //public string Client_id { get; set; }

        [Required]
        public string Company { get; set; }

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

        public string Business_number { get; set; }

        public int Id { get; set; }

    }
}
