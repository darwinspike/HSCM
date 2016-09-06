using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Storage.Models.User;
using Bussines.User;


namespace Storage.Controllers.User
{
    public class UserTypeController : Controller
    {

        #region Proerties
        private static string result = "";
        #endregion


        // GET: UserType
        #region Views
        public ActionResult UserTypeView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "User Type";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = UserTypeBussines.Select.GetUserTypeList();
            var model = new UserTypeViewModels()
            {
                Error = BussinesData.Error,
                UserTypeList = BussinesData.UserTypeList
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult UserTypeCreate()
        {
            ViewBag.Title = "New User Type";
            ViewBag.Message = "";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserTypeCreate(UserTypeViewModels data)
        {
            UserTypeBussines.GetUserTypeResponse request = new UserTypeBussines.GetUserTypeResponse()
            {
                UserType = data.UserType
            };

            result = UserTypeBussines.Insert.UserType(request).Message;

            return RedirectToAction("UserTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult UserTypeUpdate(int id)
        {
            ViewBag.Title = "Update User Type";
            ViewBag.Message = "";

            UserTypeBussines.GetUserTypeRequest request = new UserTypeBussines.GetUserTypeRequest() { UserTypeID = id };
            UserType u = UserTypeBussines.Select.GetUserType(request).UserType;

            var model = new UserTypeViewModels() {  UserType = u };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserTypeUpdate(UserTypeViewModels data)
        {
            if (ModelState.IsValid)
            {
                UserTypeBussines.GetUserTypeResponse request = new UserTypeBussines.GetUserTypeResponse()
                {
                    UserType = data.UserType
                };
                result = UserTypeBussines.Update.UserType(request).Message;
            }

            return RedirectToAction("UserTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult UserTypeDisable(int id, string state)
        {
            result = UserTypeBussines.Delete.UserTypeDisable(id, state).Message;
            return RedirectToAction("UserTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion

    }
}