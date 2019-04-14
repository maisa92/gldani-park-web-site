using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GldaniParkService.Models.HelperModels
{
    public class CardHistoryHelperModel
    {
        public int CardId { get; set; }
        public decimal CardPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal RestAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
