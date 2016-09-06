using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Storage.Models.Administration;
using Bussines;

namespace Storage.Controllers.Administration
{
    public class AssignmentTypeController : Controller
    {
        #region Proerties
        private static string result = "";
        #endregion
        // GET: AssingmentType
        #region Views
        public ActionResult AssingmentTypeView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Assingment Type";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = AssignmentTypeBussines.Select.GetAssignmentTypeList();
            var model = new AssignmentTypeViewModels()
            {
                Error = BussinesData.Error,
                AssignmentTypeList = BussinesData.AssignmentTypeList
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult AssingmentTypeCreate()
        {
            ViewBag.Title = "New Assignment Type";
            ViewBag.Message = "";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssingmentTypeCreate(AssignmentTypeViewModels data)
        {
            if (ModelState.IsValid)
            {
                AssignmentTypeBussines.GetAssignmentTypeResponse request = new AssignmentTypeBussines.GetAssignmentTypeResponse()
                {
                    AssignmentType = data.AssignmentType
                };

                result = AssignmentTypeBussines.Insert.AssignmentType(request).Message;
            }

            return RedirectToAction("AssingmentTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult AssingmentTypeUpdate(int id)
        {
            ViewBag.Title = "Update ASsignment Type";
            ViewBag.Message = "";

            AssignmentTypeBussines.GetAssignmentTypeRequest request = new AssignmentTypeBussines.GetAssignmentTypeRequest() { AssignmentTypeID = id };
            AssignmentType C = AssignmentTypeBussines.Select.GetAssignmentType(request).AssignmentType;

            var model = new AssignmentTypeViewModels() { AssignmentType = C };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssingmentTypeUpdate(AssignmentTypeViewModels data)
        {

            if (ModelState.IsValid)
            {
                AssignmentTypeBussines.GetAssignmentTypeResponse request = new AssignmentTypeBussines.GetAssignmentTypeResponse()
                {
                    AssignmentType = data.AssignmentType
                };
                result = AssignmentTypeBussines.Update.AssignmentType(request).Message;
            }

            return RedirectToAction("AssingmentTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult AssingmentTypeDisable(int id, string state)
        {
            result = AssignmentTypeBussines.Delete.AssignmentTypeDisable(id, state).Message;
            return RedirectToAction("AssingmentTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion

    }
}