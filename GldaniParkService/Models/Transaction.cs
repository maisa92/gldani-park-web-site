using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GldaniParkService.Models
{
    public class Transaction
    {
        public int? Id { get; set; }
        public bool Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string AttractionId { get; set; }
        public int? CardId { get; set; }
    }
}
