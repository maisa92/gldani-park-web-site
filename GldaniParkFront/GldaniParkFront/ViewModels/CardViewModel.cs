using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GldaniParkFront.ViewModels
{
    public class CardViewModel
    {
        public int? Id { get; set; }
        public string CardId { get; set; }
        public bool IsActive { get; set; }
        public decimal CardPrice { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}