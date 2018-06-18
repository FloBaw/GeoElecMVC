using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoElecMVC.Models
{
    public class Generator
    {
        [Required]
        public string Generator_id { get; set; }

        public string Start_date { get; set; }

        public string End_date { get; set; }

        public string Maintenance { get; set; }

        public string Installation_type { get; set; }

        public int Client_id { get; set; }

        public int Lessee_id { get; set; }

        public int Place_id { get; set; }

        public int Specification_id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Company { get; set; }

        public string Name { get; set; }

        public string First_name { get; set; }

    }
}
