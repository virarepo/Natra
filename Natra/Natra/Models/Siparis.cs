using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natra.Models
{
    public class Siparis
    {
        [JsonIgnore]
        public int mobileDbId { get; set; } // mobile siparis key-value db

        public Stok stok {get; set;}
        public string OlcuBirimi { get; set; }
        public int Miktar { get; set; }
        public string SiparisNotlari { get; set; }
        public string HesapKodu { get; set; }
        public double GenelToplam { get; set; }
        public double BrutTutar { get; set; }
        public double KDVToplam { get; set; }
    }
}
