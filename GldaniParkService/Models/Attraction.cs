using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GldaniParkService.Models
{
    public class Attraction
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public string AttractionId { get; set; }
        public decimal Price { get; set; }
    }
}
