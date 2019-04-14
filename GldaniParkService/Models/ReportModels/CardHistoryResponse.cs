using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GldaniParkService.Models.HelperModels;

namespace GldaniParkService.Models.ReportModels
{
    public class CardHistoryResponse
    {
        public CardHistoryResponse()
        {
            TransactionList = new List<Transaction>();
            AttractionList = new List<AttractionForCardHistoryModel>();
        }
        public int CardId { get; set; }
        public decimal CardPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal RestAmount { get; set; }
        public bool IsActive { get; set; }
        public List<Transaction> TransactionList { get; set; }
        public List<AttractionForCardHistoryModel> AttractionList { get; set; }
    }
}
