using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storage.Models.User;
using Bussines.User;


namespace Storage.Controllers.User
{
    public class UserController : Controller
    {
        #region Proerties
        private static string result = "";
        #endregion

        // GET: User
        #region Views
        public ActionResult UserView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Users";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = UserBussines.Select.GetUserList();
            var model = new UserViewModels()
            {
                Error = BussinesData.Error,
                UsersList = BussinesData.UserList
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult UserCreate()
        {
            ViewBag.Title = "New User";
            ViewBag.Message = "";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate(UserViewModels data)
        {
            UserBussines.GetUserResponse request = new UserBussines.GetUserResponse()
            {
                User = data.Users
            };

            result = UserBussines.Insert.User(request).Message;

            return RedirectToAction("UserView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult UserUpdate(int id)
        {
            ViewBag.Title = "Update Cellar Area";
            ViewBag.Message = "";

            UserBussines.GetUserRequest request = new UserBussines.GetUserRequest() { UserID = id };
            Users u = UserBussines.Select.GetUser(request).User;

            var model = new UserViewModels() { Users = u };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserUpdate(UserViewModels data)
        {
            UserBussines.GetUserResponse request = new UserBussines.GetUserResponse()
            {
                User = data.Users
            };
            result = UserBussines.Update.User(request).Message;

            return RedirectToAction("UserView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult UserDisable(int id, string state)
        {
            result = UserBussines.Delete.UserDisable(id, state).Message;
            return RedirectToAction("UserView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion

    }
}