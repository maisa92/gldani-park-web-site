using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GldaniParkFront.ViewModels;
using GldaniParkService.Reports;
using GldaniParkService.Services;

namespace GldaniParkFront.Controllers
{
    public class CardController : Controller
    {
        // GET: Cards
        public ActionResult Index()
        {
            OperationService op = new OperationService();
            var modelList = op.GetCards();
            var returnModelList = modelList?.Select(x => new CardViewModel()
            {
                CreateDate = x.CreateDate,
                Id = x.Id,
                IsActive = x.IsActive,
                CardId = x.CardId,
                CardPrice = x.CardPrice

            }) ?? new List<CardViewModel>();

            return View(returnModelList);

        }

        // GET: Cards/Details/5
        public ActionResult Details(int id)
        {
            OperationService os = new OperationService();
            var card = os.GetCard(id);


            ReportService rs = new ReportService();
            var cardHistory = rs.CardHistory(card.CardId);

            return View(cardHistory);
        }





    }
}
