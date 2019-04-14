using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GldaniParkFront.ViewModels;
using GldaniParkService.Models;
using GldaniParkService.Services;

namespace GldaniParkFront.Controllers
{
    public class AttractionController : Controller
    {
        // GET: Attractions
        public ActionResult Index()
        {
            OperationService op = new OperationService();
            var modelList = op.GetAttractions();
            var returnModelList = modelList?.Select(x => new AttractionViewModel()
            {
                CreateDate = x.CreateDate,
                Id = x.Id,
                Price = x.Price,
                Name = x.Name,
                AttractionId = x.AttractionId
            }) ?? new List<AttractionViewModel>();

            return View(returnModelList);
        }

        // GET: Attractions/Details/5
        public ActionResult Details(int id)
        {
            OperationService op = new OperationService();
            var attraction = op.GetAttraction(id);
            return View(attraction);
        }

        // GET: Attractions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attractions/Create
        [HttpPost]
        public ActionResult Create(AttractionViewModel attractionViewModel)
        {
            try
            {
                OperationService op = new OperationService();
                var attractionList = op.GetAttractions();

             


                Attraction attraction = new Attraction
                {
                    Name = attractionViewModel.Name,
                    Price = attractionViewModel.Price,
                    AttractionId = attractionList == null || !attractionList.Any() ? 
                                             "01"  : Convert.ToInt32(attractionList.Max(x => x.AttractionId)) >= 9 ? (Convert.ToInt32(attractionList.Max(x => x.AttractionId)) + 1).ToString() : $"0{Convert.ToInt32(attractionList.Max(x => x.AttractionId)) + 1}"
                };

                if (!op.SaveAttraction(attraction))
                    throw new Exception();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Attractions/Edit/5
        public ActionResult Edit(int id)
        {
            OperationService op = new OperationService();
            var attraction = op.GetAttraction(id);
            return View(attraction);
           
        }

        // POST: Attractions/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Attraction attraction)
        {
            try
            {
                OperationService op = new OperationService();
                if (!op.UpdateAttraction(attraction))
                    throw new Exception();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Attractions/Delete/5
        public ActionResult Delete(int id)
        {
            OperationService op = new OperationService();
            var attraction = op.GetAttraction(id);
            return View(attraction);
        }

        // POST: Attractions/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                OperationService op = new OperationService();
                 if(!op.DeleteTransaction(id))
                    throw new Exception();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
