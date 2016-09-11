using Bussines;
using Bussines.User;
using Storage.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Bussines.User.UserConfigurationBussines;

namespace Storage.Controllers.User
{
    public class UserConfigurationController : Controller
    {
        #region Proerties
        private static string result = "";
        #endregion

        #region Views
        public ActionResult UserPermissionView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Users Permission";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = UserBussines.Select.GetUserButConfigurationList();
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
        public ActionResult UserPermissionCreate()
        {
            ViewBag.Title = "New User Permission";
            ViewBag.Message = "";

            var model = new UserConfigurationViewModels()
            {
                User = UserBussines.Select.GetUserButNotConfigurationList().UserList,
                TypePermission = TypePermissionBussines.Select.GetTypePermissionList().TypePermissionList,
                AssignmentType = AssignmentTypeBussines.Select.GetAssignmentTypeList().AssignmentTypeList,
                CellarArea = CellarAreaBussines.Select.GetCellarAreaList().CellarAreaList,
                Category = CategoryBussines.Select.GetCategory().CategoryList,
                Department = DepartmentBussines.Select.GetDepartmentList().DepartmentList,
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserPermissionCreate(UserConfigurationViewModels data)
        {
            switch (data.UserConfiguration.idCellarArea)
            {
                case 1:
                    data.UserConfiguration.idAssignmentType = 1;
                    data.UserConfiguration.idAnchorAssignmentType = data.UserConfiguration.DepartmentID;
                    break;
                default:
                    data.UserConfiguration.idAssignmentType = 2;
                    data.UserConfiguration.idAnchorAssignmentType = data.UserConfiguration.CategoryID;
                    break;
            }

            UserConfigurationBussines.GetUserConfigurationResponse request = new UserConfigurationBussines.GetUserConfigurationResponse()
            {
                UserConfiguration = data.UserConfiguration
            };

            result = UserConfigurationBussines.Insert.UserConfiguration(request).Message;

            return RedirectToAction("UserPermissionView", new { successful = true, ResultAction = "All Changes was successful" });
        }


        public ActionResult UserPermissionNewAssignment(int id) {
            var model = new UserConfigurationViewModels()
            {
                UserConfiguration = new UserConfiguration() { idUser = id },
                User = UserBussines.Select.GetUserList().UserList,
                TypePermission = TypePermissionBussines.Select.GetTypePermissionList().TypePermissionList,
                AssignmentType = AssignmentTypeBussines.Select.GetAssignmentTypeList().AssignmentTypeList,
                CellarArea = CellarAreaBussines.Select.GetCellarAreaList().CellarAreaList,
                Category = CategoryBussines.Select.GetCategory().CategoryList,
                Department = DepartmentBussines.Select.GetDepartmentList().DepartmentList
            };

            ViewBag.Title = "New User Permission To " + UserBussines.Select.GetUserName(id).User.user;
            ViewBag.Message = "";


            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserPermissionNewAssignment(UserConfigurationViewModels data)
        {
            switch (data.UserConfiguration.idCellarArea)
            {
                case 1:
                    data.UserConfiguration.idAssignmentType = 1;
                    data.UserConfiguration.idAnchorAssignmentType = data.UserConfiguration.DepartmentID;
                    break;
                default:
                    data.UserConfiguration.idAssignmentType = 2;
                    data.UserConfiguration.idAnchorAssignmentType = data.UserConfiguration.CategoryID;
                    break;
            }


            UserConfigurationBussines.GetUserConfigurationResponse request = new UserConfigurationBussines.GetUserConfigurationResponse()
            {
                UserConfiguration = data.UserConfiguration
            };

            result = UserConfigurationBussines.Insert.UserConfiguration(request).Message;

            return RedirectToAction("UserPermissionView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion

        #region Detail
        [HttpGet]
        public ActionResult UserPermissionDetail(int id) {
            GetUserConfigurationUserIDRequest request = new GetUserConfigurationUserIDRequest()
            {
                UserID = id
            };

            var BussinesData = UserConfigurationBussines.Select.GetUserConfigurationListByUserID(request);
            var model = new UserConfigurationViewModels()
            {
                Error = BussinesData.Error,
                UserConfigurationList = BussinesData.UserConfigurationList
            };

            ViewBag.Title = "Detail User Permission To " + UserBussines.Select.GetUserName(id).User.user;
            ViewBag.Message = "";

            return PartialView(model);
        }

        #endregion

        #region Update
        [HttpGet]
        public ActionResult UserPermissionUpdate(int id)
        {
            UserConfigurationBussines.GetUserConfigurationRequest request = new UserConfigurationBussines.GetUserConfigurationRequest() {
                UserConfigurationID = id
            };
            UserConfiguration u = UserConfigurationBussines.Select.GetUserConfiguration(request).UserConfiguration;

            var model = new UserConfigurationViewModels()
            {
                UserConfiguration = u,
                User = UserBussines.Select.GetUserList().UserList,
                TypePermission = TypePermissionBussines.Select.GetTypePermissionList().TypePermissionList,
                AssignmentType = AssignmentTypeBussines.Select.GetAssignmentTypeList().AssignmentTypeList,
                CellarArea = CellarAreaBussines.Select.GetCellarAreaList().CellarAreaList,
                Category = CategoryBussines.Select.GetCategory().CategoryList,
                Department = DepartmentBussines.Select.GetDepartmentList().DepartmentList
            };

            GetUserConfigurationRequest request2 = new GetUserConfigurationRequest()
            {
                UserConfigurationID = id
            };
            int BussinesData2 = (int)UserConfigurationBussines.Select.GetUserConfiguration(request2).UserConfiguration.idUser;
            ViewBag.Title = "Update User Configuration To " + UserBussines.Select.GetUserName(BussinesData2).User.user;
            ViewBag.Message = "";



            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserPermissionUpdate(UserConfigurationViewModels data)
        {
            if (ModelState.IsValid)
            {
                UserConfigurationBussines.GetUserConfigurationResponse request = new UserConfigurationBussines.GetUserConfigurationResponse()
                {
                    UserConfiguration = data.UserConfiguration
                };
                result = UserConfigurationBussines.Update.UserConfiguration(request).Message;
            }

            return RedirectToAction("UserPermissionView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult UserPermissionDisable(int id, string state)
        {
            result = UserConfigurationBussines.Delete.UserConfigurationDisable(id, state).Message;
            return RedirectToAction("UserPermissionView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion

    }
}