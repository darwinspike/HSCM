using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Storage.Models.User;
using Bussines.User;

namespace Storage.Controllers.User
{
    public class TypePermissionController : Controller
    {

        #region Proerties
        private static string result = "";
        #endregion

        // GET: TypePermission
        #region Views
        public ActionResult PermissionTypeView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Permission Type";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = TypePermissionBussines.Select.GetTypePermissionList();
            var model = new TypePermissionViewModels()
            {
                Error = BussinesData.Error,
                TypePermissionList = BussinesData.TypePermissionList
            };

            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult PermissionTypeUpdate(int id)
        {
            ViewBag.Title = "Update User Permission Type";
            ViewBag.Message = "";

            TypePermissionBussines.GetTypePermissionRequest request = new TypePermissionBussines.GetTypePermissionRequest() { TypePermissionID = id };
            TypePermission C = TypePermissionBussines.Select.GetTypePermission(request).TypePermission;

            var model = new TypePermissionViewModels() { TypePermission = C };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PermissionTypeUpdate(TypePermissionViewModels data)
        {

            if (ModelState.IsValid)
            {
                TypePermissionBussines.GetTypePermissionResponse request = new TypePermissionBussines.GetTypePermissionResponse()
                {
                    TypePermission = data.TypePermission
                };
                result = TypePermissionBussines.Update.TypePermission(request).Message;
            }

            return RedirectToAction("PermissionTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult PermissionTypeDisable(int id, string state)
        {
            result = TypePermissionBussines.Delete.TypePermissionDisable(id, state).Message;
            return RedirectToAction("PermissionTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion

    }
}
