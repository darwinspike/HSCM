using Bussines.Product;
using Bussines.FileManager;
using Storage.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storage.Controllers.Product
{
    public class ProductController : Controller
    {

        #region Proerties
        private static string result = "";
        #endregion

        #region Views
        public ActionResult productView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Products";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = ProductBussines.Select.GetProduct();
            var model = new ProductViewModels()
            {
                Error = BussinesData.Error,
                ProductsList = BussinesData.ProductList,
                ProductTypeList = ProductTypeBussines.Select.GetProductTypeList().ProductTypeList
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult productCreate()
        {
            ViewBag.Title = "New Product";
            ViewBag.Message = "";

            var model = new ProductViewModels()
            {
                ProductTypeList = ProductTypeBussines.Select.GetProductTypeList().ProductTypeList

            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult productCreate(ProductViewModels data, IEnumerable<HttpPostedFileBase> files)
        {
            ProductBussines.GetProductResponse p = new ProductBussines.GetProductResponse() { Product = data.Products };

            if (files.First() != null)
            {
                ProductFileManagerBussines.GetProductFileManagerFileRequest request = new ProductFileManagerBussines.GetProductFileManagerFileRequest()
                {
                    product = p,
                    files = files
                };
                ProductFileManagerBussines.Insert.SaveProductWithFileManager(request);
            }
            else
            {
                result = ProductBussines.Insert.Product(p).Message;

            }

            return RedirectToAction("productView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult productEdit(int id)
        {
            ViewBag.Title = "Update Product";
            ViewBag.Message = "";

            ProductBussines.GetProductRequest request = new ProductBussines.GetProductRequest() { ProductID = id };
            FileManagerBussines.GetFileManagerRequest requesFile = new FileManagerBussines.GetFileManagerRequest() { FileManagerID = id };
            Products p = ProductBussines.Select.GetProduct(request).Product;

            var model = new ProductViewModels()
            {
                Products = p,
                ProductTypeList = ProductTypeBussines.Select.GetProductTypeList().ProductTypeList,
                FileManagersList = FileManagerBussines.Select.GetProductFileManager(requesFile).FileManagerList
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult productEdit(ProductViewModels data, IEnumerable<HttpPostedFileBase> files)
        {

            if (ModelState.IsValid)
            {
                ProductBussines.GetProductResponse p = new ProductBussines.GetProductResponse() { Product = data.Products };
                result = ProductBussines.Update.Product(p).Message;
            }

            return RedirectToAction("productView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Disable
        public ActionResult productDisable(int id, string state)
        {
            result = ProductBussines.Delete.ProductDisable(id, state).Message;
            return RedirectToAction("productView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        [HttpGet]
        public ActionResult DownLoad(int id)
        {

            ProductBussines.GetProductRequest request = new ProductBussines.GetProductRequest() { ProductID = id };
            FileManagerBussines.GetFileManagerRequest requesFile = new FileManagerBussines.GetFileManagerRequest() { FileManagerID = id };
            Products p = ProductBussines.Select.GetProduct(request).Product;

            var model = new ProductViewModels()
            {
                Products = p,
                ProductTypeList = ProductTypeBussines.Select.GetProductTypeList().ProductTypeList,
                FileManagersList = FileManagerBussines.Select.GetProductFileManager(requesFile).FileManagerList
            };
            return PartialView(model);
        }


    }
}