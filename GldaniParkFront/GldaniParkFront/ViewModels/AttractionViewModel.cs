using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GldaniParkFront.ViewModels
{
    public class AttractionViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public string AttractionId { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal Price { get; set; }
    }
}