using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoElecMVC.Models
{
    public class Vault
    {
        public string log_id { get; set; }
        public string log_timestamp { get; set; }
        public string log_date { get; set; }
        public string generator_id { get; set; }
        public string nrj_tot { get; set; }
        public string power_1 { get; set; }
        public string power_2 { get; set; }
        public string power_3 { get; set; }
        public string current_1 { get; set; }
        public string current_2 { get; set; }
        public string current_3 { get; set; }
        public string voltage { get; set; }
        public string relay_1 { get; set; }
        public string relay_2 { get; set; }
        public string magnetic { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string payment { get; set; }
    }
}
