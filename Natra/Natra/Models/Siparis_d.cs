using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natra.Models
{
    public class Siparis_d
    {
        [JsonIgnore]
        public int mobileDbId { get; set; } // mobile siparis key-value db

        public Stok stok {get; set;}
        public string OlcuBirimi { get; set; }
        public int Miktar { get; set; }
        public string HesapKodu { get; set; }
        public double BrutTutar { get; set; }
    }
}
