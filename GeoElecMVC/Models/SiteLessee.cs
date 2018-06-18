using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GeoElecMVC.Models
{
    public class SiteLessee
    {
        [Key]
        public int Sitelessee_id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int Lessee_id { get; set; }

        public string Name { get; set; }

        public string First_name { get; set; }

        public int Id { get; set; }
    }
}
