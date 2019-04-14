using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GldaniParkService.Models.ReportModels
{
    public class CardsSellResponse
    {
        public decimal Price { get; set; }
        public int SellAmount { get; set; }
        public int ReversedAmount { get; set; }
    }
}
