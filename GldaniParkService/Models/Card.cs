using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GldaniParkService.Models
{
    public class Card
    {
        public int? Id { get; set; }
        public string CardId { get; set; }
        public bool IsActive { get; set; }
        public decimal CardPrice { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
