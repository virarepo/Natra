using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natra.Models
{
    class Product
    {
        public string Name { get; set; }
        public string AmountType { get; set; } // kg,adet,kasa vs
        public int StockAmount { get; set; }
        public int OrderAmount { get; set; }
        public string PricePerCapita { get; set; }
        //public Seller Seller { get; set; }
    }
}
