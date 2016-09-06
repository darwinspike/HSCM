using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Storage.Models.Administration;
using Bussines;


namespace Storage.Controllers.Administration
{
    public class CellarAreaController : Controller
    {
        #region Proerties
        private static string result = "";
        #endregion
        // GET: CellarArea
        #region Views
        public ActionResult cellarAreaView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Cellar Area";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = CellarAreaBussines.Select.GetCellarAreaList();
            var model = new CellarAreaViewModels()
            {
                Error = BussinesData.Error,
                CellarAreaList = BussinesData.CellarAreaList
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult cellarAreaCrate()
        {
            ViewBag.Title = "New Cellar Area";
            ViewBag.Message = "";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cellarAreaCrate(CellarAreaViewModels data)
        {
            CellarAreaBussines.GetCellarAreaResponse request = new CellarAreaBussines.GetCellarAreaResponse()
            {
                CellarArea = data.CellarArea
            };

            result = CellarAreaBussines.Insert.CellarArea(request).Message;

            return RedirectToAction("cellarAreaView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult cellarAreaUpdate(int id)
        {
            ViewBag.Title = "Update Cellar Area";
            ViewBag.Message = "";

            CellarAreaBussines.GetCellarAreaRequest request = new CellarAreaBussines.GetCellarAreaRequest() { CellarAreaID = id };
            CellarArea C = CellarAreaBussines.Select.GetCellarArea(request).CellarArea;

            var model = new CellarAreaViewModels() { CellarArea = C };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cellarAreaUpdate(CellarAreaViewModels data)
        {

            if (ModelState.IsValid)
            {
                CellarAreaBussines.GetCellarAreaResponse request = new CellarAreaBussines.GetCellarAreaResponse()
                {
                    CellarArea = data.CellarArea
                };
                result = CellarAreaBussines.Update.CellarArea(request).Message;
            }

            return RedirectToAction("cellarAreaView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult cellarAreaDisable(int id, string state)
        {
            result = CellarAreaBussines.Delete.CellarAreaDisable(id, state).Message;
            return RedirectToAction("cellarAreaView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion

    }
}