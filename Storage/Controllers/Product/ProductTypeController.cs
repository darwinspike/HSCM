using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Storage.Models.Product;
using Bussines.Product;

namespace Storage.Controllers.Product
{
    public class ProductTypeController : Controller
    {

        #region Proerties
        private static string result = "";
        #endregion



        // GET: UserType
        #region Views
        public ActionResult productTypeView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Produc Type";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = ProductTypeBussines.Select.GetProductTypeList();
            var model = new ProductTypeViewModels()
            {
                Error = BussinesData.Error,
                ProductTypeList = BussinesData.ProductTypeList
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult productTypeCreate()
        {
            ViewBag.Title = "New Product TYpe";
            ViewBag.Message = "";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult productTypeCreate(ProductTypeViewModels data)
        {
            ProductTypeBussines.GetProductTypeResponse request = new ProductTypeBussines.GetProductTypeResponse()
            {
                ProductType = data.ProductType
            };

            result = ProductTypeBussines.Insert.ProductType(request).Message;

            return RedirectToAction("productTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult productTypeEdit(int id)
        {
            ViewBag.Title = "Update Product Type";
            ViewBag.Message = "";

            ProductTypeBussines.GetProductTypeRequest request = new ProductTypeBussines.GetProductTypeRequest() { ProductTypeID = id };
            ProductType p = ProductTypeBussines.Select.GetProductType(request).ProductType;

            var model = new ProductTypeViewModels() { ProductType = p };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult productTypeEdit(ProductTypeViewModels data)
        {
            if (ModelState.IsValid)
            {
                ProductTypeBussines.GetProductTypeResponse request = new ProductTypeBussines.GetProductTypeResponse()
                {
                    ProductType = data.ProductType
                };
                result = ProductTypeBussines.Update.ProductType(request).Message;
            }

            return RedirectToAction("productTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult productTypeDisable(int id, string state)
        {
            result = ProductTypeBussines.Delete.ProductTypeDisable(id, state).Message;
            return RedirectToAction("productTypeView", new { successful = true, ResultAction = "All Changes was successful" });
        }

        #endregion


    }
}