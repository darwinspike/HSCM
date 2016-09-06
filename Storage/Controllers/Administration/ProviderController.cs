using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Storage.Models.Administration;
using Bussines;


namespace Storage.Controllers.Administration
{
    public class ProviderController : Controller
    {

        #region Proerties
        private static string result = "";
        #endregion

        // GET: Provider
        #region View
        public ActionResult providerView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Provider";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = ProviderBussines.Select.GetProviderList();
            var model = new ProviderViewModels()
            {
                Error = BussinesData.Error,
                ProviderList = BussinesData.ProviderList
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult providerCreate()
        {
            ViewBag.Title = "New Provider";
            ViewBag.Message = "";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult providerCreate(ProviderViewModels data)
        {
            ProviderBussines.GetProviderResponse request = new ProviderBussines.GetProviderResponse()
            {
                Provider = data.Provider
            };

            result = ProviderBussines.Insert.Provider(request).Message;
            return RedirectToAction("providerView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult providerUpdate(int id)
        {
            ViewBag.Title = "Update Provider";
            ViewBag.Message = "";

            ProviderBussines.GetProviderRequest request = new ProviderBussines.GetProviderRequest() { ProviderID = id };
            Provider p = ProviderBussines.Select.GetProvider(request).Provider;

            var model = new ProviderViewModels() { Provider = p };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult providerUpdate(ProviderViewModels data)
        {
            if (ModelState.IsValid)
            {
                ProviderBussines.GetProviderResponse request = new ProviderBussines.GetProviderResponse()
                {
                    Provider = data.Provider
                };
                result = ProviderBussines.Update.Provider(request).Message;
            }

            return RedirectToAction("providerView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult providerDisable(int id, string state)
        {
            result = ProviderBussines.Delete.ProviderDisable(id, state).Message;
            return RedirectToAction("providerView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion



    }
}