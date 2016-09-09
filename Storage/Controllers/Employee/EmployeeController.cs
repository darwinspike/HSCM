using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Storage.Models.Employee;
using Bussines.Employee;
using Bussines.User;


namespace Storage.Controllers.Employee
{
    public class EmployeeController : Controller
    {


        #region Proerties
        private static string result = "";
        #endregion

        #region Views
        public ActionResult EmployeeView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Employees";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = EmployeeBussines.Select.GetEmployeeList();
            var model = new EmployeeViewModels()
            {
                Error = BussinesData.Error,
                EmployeeList = BussinesData.EmployeeList
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult EmployeeCreate()
        {
            ViewBag.Title = "New Employee";
            ViewBag.Message = "";

            var model = new EmployeeViewModels()
            {
                User = UserBussines.Select.GetUserList().UserList,
                UserType = UserTypeBussines.Select.GetUserTypeList().UserTypeList
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeCreate(EmployeeViewModels data)
        {
            EmployeeBussines.GetEmployeeResponse request = new EmployeeBussines.GetEmployeeResponse()
            {
                Employee = data.Employee
            };

            result = EmployeeBussines.Insert.Employee(request).Message;

            return RedirectToAction("EmployeeView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult EmployeeUpdate(int id)
        {
            ViewBag.Title = "Update Employee";
            ViewBag.Message = "";

            EmployeeBussines.GetEmployeeRequest request = new EmployeeBussines.GetEmployeeRequest() { EmployeeID = id };
            Employees C = EmployeeBussines.Select.GetEmployee(request).Employee;

            var model = new EmployeeViewModels() {
                Employee = C,
                User = UserBussines.Select.GetUserList().UserList,
                UserType = UserTypeBussines.Select.GetUserTypeList().UserTypeList
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeUpdate(EmployeeViewModels data)
        {

            if (ModelState.IsValid)
            {
                EmployeeBussines.GetEmployeeResponse request = new EmployeeBussines.GetEmployeeResponse()
                {
                    Employee = data.Employee
                };
                result = EmployeeBussines.Update.Employee(request).Message;
            }

            return RedirectToAction("EmployeeView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult EmployeeDisable(int id, string state)
        {
            result = EmployeeBussines.Delete.EmployeeDisable(id, state).Message;
            return RedirectToAction("EmployeeView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion



    }
}