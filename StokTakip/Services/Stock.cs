using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Services
{
    public class Stock
    {
        public string StockNumber { get; set; }
        public string StockCategory { get; set; }
        public string StockName { get; set; }
        public string StockUnit { get; set; }
        public int StockLogin { get; set; }
        public int StockOut { get; set; }
        public int StockLast { get; set; }
    }

}
