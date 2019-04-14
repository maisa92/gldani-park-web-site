using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GldaniParkService.Models.ReportModels
{
    public class DistributionOfTheAmountOfMoneyReceivedInMonthsResponse
    {
        public decimal Income { get; set; }
        public DateTime Date { get; set; }
    }
    public class DistributionOfTheAmountOfMoneyReceivedInMonthsAmount
    {
        public decimal Amount { get; set; }
    }

}
