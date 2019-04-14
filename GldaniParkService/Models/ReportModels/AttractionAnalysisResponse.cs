using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GldaniParkService.Models.ReportModels
{
   public class AttractionAnalysisResponse
    {
        public AttractionAnalysisResponse()
        {
            AnalysList = new List<AttractionAnalysis>();
        }
        public List<AttractionAnalysis> AnalysList { get; set; }
        public int ActiveAttractionCount { get; set; }
    }

    public class AttractionAnalysis
    {
        public string Name { get; set; }
        public decimal Amount { get; set; } 
    }
}
