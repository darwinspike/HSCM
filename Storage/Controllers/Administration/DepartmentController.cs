using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Storage.Models.Administration;
using Bussines;

namespace Storage.Controllers.Administration
{
    public class DepartmentController : Controller
    {
        #region Proerties
        private static string result = "";
        #endregion
        // GET: Department
        #region Views

        public ActionResult departmentView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Department";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = DepartmentBussines.Select.GetDepartmentList();
            var model = new DepartmentViewModels()
            {
                Error = BussinesData.Error,
                DepartmentList = BussinesData.DepartmentList
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult departmentCreate()
        {
            ViewBag.Title = "New Department";
            ViewBag.Message = "";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult departmentCreate(DepartmentViewModels data)
        {
            if (ModelState.IsValid)
            {
                DepartmentBussines.GetDepartmentResponse request = new DepartmentBussines.GetDepartmentResponse() {
                    Department = data.Department
                };

                result = DepartmentBussines.Insert.Department(request).Message;
            }

            return RedirectToAction("departmentView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult departmentUpdate(int id)
        {
            ViewBag.Title = "Update Department";
            ViewBag.Message = "";
            DepartmentBussines.GetDepartmentRequest request = new DepartmentBussines.GetDepartmentRequest() { DepartmentID = id };
            Department d = DepartmentBussines.Select.GetDepartment(request).Department;
            var model = new DepartmentViewModels(){ Department = d };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult departmentUpdate(DepartmentViewModels data)
        {

            if (ModelState.IsValid)
            {
                DepartmentBussines.GetDepartmentResponse request = new DepartmentBussines.GetDepartmentResponse()
                {
                    Department = data.Department
                };
                result = DepartmentBussines.Update.Department(request).Message;
            }

            return RedirectToAction("departmentView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult departmentDisable(int id, string state)
        {
            result = DepartmentBussines.Delete.DepartmentDisable(id, state).Message;
            return RedirectToAction("departmentView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion

    }
}