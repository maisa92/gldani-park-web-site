using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GldaniParkService.Models.ReportModels
{
    public class HourlyDistributionOfFundsResponse
    {
        public string WeekDay { get; set; }

        public string Hour { get; set; }

        public decimal Income { get; set; }

        public int CardAmount { get; set; }

        public decimal OutCome { get; set; }

        public int ActingCardAmount { get; set; }

        public int CardId { get; set; }
    }
}
