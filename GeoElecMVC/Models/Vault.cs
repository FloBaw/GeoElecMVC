using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoElecMVC.Models
{
    public class Vault
    {
        public long log_id { get; set; }
        public string log_timestamp { get; set; }
        public DateTime log_date { get; set; }
        public string generator_id { get; set; }
        public double nrj_tot { get; set; }
        public double power_1 { get; set; }
        public double power_2 { get; set; }
        public double power_3 { get; set; }
        public double current_1 { get; set; }
        public double current_2 { get; set; }
        public double current_3 { get; set; }
        public double voltage { get; set; }
        public bool relay_1 { get; set; }
        public bool relay_2 { get; set; }
        public bool magnetic { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool payment { get; set; }
    }
}
