using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GldaniParkFront.ViewModels;
using GldaniParkService.Models.ReportModels;
using GldaniParkService.Reports;

namespace GldaniParkFront.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            List<ReportListViewModel> actionList  = new List<ReportListViewModel>()
            {
                new ReportListViewModel(){ Action = "ExpenseAnalysis", Name =  "ხარჯების ანალიზი"},
                new ReportListViewModel(){ Action = "IncomeExpenseAnalysis", Name =  "შემოსული თანხების ანალიზი"},
                new ReportListViewModel(){ Action = "HourlyDistributionOfFunds", Name =  "თანხების საათობრივი განაწილება"},
                new ReportListViewModel(){ Action = "DistributionOfTheAmountOfMoneyReceivedInMonths", Name =  "შემოსული თანხების გადანაწილება თვეებზე"},
                new ReportListViewModel(){ Action = "CardsSell", Name =  "ბარათის გაყიდვა"},
                new ReportListViewModel(){ Action = "MoneyMovement", Name =  "ფულის მოძრაობა ბარათზე"}, //araaaa
              //  new ReportListViewModel(){ Action = "CardHistory", Name =  "ბარათის ისტორია"},
              //  new ReportListViewModel(){ Action = "CardTransactions", Name =  "ბარათის ტრანზაქციები"},
             //   new ReportListViewModel(){ Action = "AttractionAnalysis", Name =  "ატრაქციონებზე თამაშის ანალიზი"},
                new ReportListViewModel(){ Action = "AttractionRank", Name =  "აპარატების რეიტინგი"}
            };

            return View(actionList);
        }
        
        public ActionResult AttractionRank()
        {
            ReportService rs = new ReportService();
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1);
            ViewBag.EndDate = DateTime.Now.Date;

            var attractionRank = rs.AttractionRank(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now.Date);

            ViewBag.ActiveAttraction = attractionRank.Count(x => x.Amount != 0);
            return View(attractionRank.OrderByDescending(x => x.Amount));
        }

        [HttpPost]
        public ActionResult AttractionRank(DateTime startDate, DateTime endDate)
        {
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
          
            ReportService rs = new ReportService();
            var attractionRank = rs.AttractionRank(startDate, endDate);

            ViewBag.ActiveAttraction = attractionRank.Count(x => x.Amount != 0);
            return View(attractionRank.OrderByDescending(x => x.Amount));
        }



        public ActionResult CardsSell()
        {
            ReportService rs = new ReportService();
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1);
            ViewBag.EndDate = DateTime.Now.Date;

            var cardsSell = rs.CardsSell(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now.Date);
             
            return View(cardsSell);
        }

        [HttpPost]
        public ActionResult CardsSell(DateTime startDate, DateTime endDate)
        {
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            ReportService rs = new ReportService();
            var cardsSell = rs.CardsSell(startDate, endDate);
             
            return View(cardsSell);
        }



        public ActionResult HourlyDistributionOfFunds()
        {
            ReportService rs = new ReportService();
            ViewBag.StartDate = new DateTime(DateTime.Now.Year, 1, 1);
            ViewBag.EndDate = DateTime.Now.Date;

            var hourlyDistributionOfFunds = rs.HourlyDistributionOfFunds(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now.Date);

            return View(hourlyDistributionOfFunds);
        }

        


        public ActionResult DistributionOfTheAmountOfMoneyReceivedInMonths()
        {
            ReportService rs = new ReportService();

            ViewBag.Year = DateTime.Now.Year;
            ViewBag.Month = DateTime.Now.Month;

            ViewBag.YearList = GetYearSelectList();
            ViewBag.MonthList = GetMonthSelectList();


            var distributionOfTheAmountOfMoneyReceivedInMonths = rs.DistributionOfTheAmountOfMoneyReceivedInMonths(DateTime.Now.Year,DateTime.Now.Month);

            return View(distributionOfTheAmountOfMoneyReceivedInMonths);
        }


        private SelectList GetYearSelectList()
        {
           
            var listSelectItems = new List<SelectListItem>();

            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year; i++)
            {
                listSelectItems.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = i == DateTime.Now.Year ? true : false
                });
            }
            var yearList = new SelectList(listSelectItems);

            return yearList;
        }


        private SelectList GetMonthSelectList()
        {
            string [] monthList = new string[]
            {
                "იანვარი",
                "თებერვალი",
                "მარტი",
                "აპრილი",
                "მაისი",
                "ივნისი",
                "ივლისი",
                "აგვისტო",
                "სექტემბერი",
                "ოქტომბერი",
                "ნოემბერი",
                "დეკემბერი"
            }; 

            var listSelectItems = new List<SelectListItem>();

            for (int i = 0; i < 12; i++)
            {
                listSelectItems.Add(new SelectListItem()
                {
                    Text = monthList[i],
                    Value = (i + 1).ToString(),
                    Selected = i + 1 == DateTime.Now.Month ? true : false
                });
            }
            var monthSelectList = new SelectList(listSelectItems);

            return monthSelectList;
        }




        [HttpPost]
        public ActionResult DistributionOfTheAmountOfMoneyReceivedInMonths(int year, int month)
        {
            ReportService rs = new ReportService();

            ViewBag.Year = DateTime.Now.Year;
            ViewBag.Month = DateTime.Now.Month;

            ViewBag.YearList = GetYearSelectList();
            ViewBag.MonthList = GetMonthSelectList();

            var distributionOfTheAmountOfMoneyReceivedInMonths = rs.DistributionOfTheAmountOfMoneyReceivedInMonths(year, month);

            return View(distributionOfTheAmountOfMoneyReceivedInMonths);
        }

    }
}
