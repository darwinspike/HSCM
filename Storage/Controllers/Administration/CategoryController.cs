using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Storage.Models.Administration;
using Bussines;




namespace Storage.Controllers.Administration
{
    public class CategoryController : Controller
    {

        #region Proerties
        private static string result = "";
        #endregion

        // GET: Category
        #region Views
        public ActionResult CategoryView(bool successful = false, string ResultAction = "")
        {

            ViewBag.Title = "Category";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = CategoryBussines.Select.GetCategory();
            var model = new CategoryViewModels();
            model.Error = BussinesData.Error;
            model.CategoryList = CategoryBussines.Select.CategoryListOrderByFatherAndChild(BussinesData.CategoryList);

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult CategoryCreate()
        {
            ViewBag.Title = "New Category";
            ViewBag.Message = "";

            var modal = new CategoryViewModels();
            var BussinesData = CategoryBussines.Select.GetCategoryToFather();
            modal.CategoryList = BussinesData.CategoryList;
            modal.Error = new Bussines.Handler.ErrorObject();

            return PartialView(modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreate(CategoryViewModels data)
        {
            CategoryBussines.GetCategoryResponse request = new CategoryBussines.GetCategoryResponse()
            {
                Category = data.Category
            };

            result = CategoryBussines.Insert.Category(request).Message;
            return RedirectToAction("CategoryView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update

        public ActionResult CategoryUpdate(int id)
        {
            ViewBag.Title = "Update Category";
            ViewBag.Message = "";
            CategoryBussines.GetCategoryRequest request = new CategoryBussines.GetCategoryRequest() { CategoryID = id };
            Category c = CategoryBussines.Select.GetCategory(request).Category;
            var model = new CategoryViewModels() {
                Category = c,                
                containerChild = CategoryBussines.Select.ContainerChild(request).IfFather,
                CategoryList = CategoryBussines.Select.GetCategoryToFather().CategoryList
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryUpdate(CategoryViewModels data)
        {
            CategoryBussines.GetCategoryResponse request = new CategoryBussines.GetCategoryResponse()
            {
                Category = data.Category
            };
            CategoryBussines.Update.Category(request);

            return RedirectToAction("CategoryView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult CategoryDisable(int id, string state)
        {
            CategoryBussines.Delete.CategoryDisable(id, state);
            return RedirectToAction("CategoryView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

    }



}
