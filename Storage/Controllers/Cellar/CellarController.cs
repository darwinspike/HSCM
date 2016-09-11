using Bussines;
using Bussines.Cellar;
using Bussines.Product;
using Bussines.Transaction;
using Storage.Models.Cellar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storage.Controllers.Cellar
{
    public class CellarController : Controller
    {

        #region Proerties
        private static string result = "";
        #endregion

        #region Views
        public ActionResult CellarView(bool successful = false, string ResultAction = "")
        {
            ViewBag.Title = "Cellar";
            ViewBag.successful = successful;
            ViewBag.Message = ResultAction;

            var BussinesData = CellarBussines.Select.GetCellar();
            var model = new CellarViewModels()
            {
                Error = BussinesData.Error,
                CellarList = BussinesData.CellarList,
                ProductList = ProductBussines.Select.GetProduct().ProductList,
                ProductTypeList = ProductTypeBussines.Select.GetProductTypeList().ProductTypeList,
                CellarAreaList = CellarAreaBussines.Select.GetCellarAreaList().CellarAreaList
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult CellarDetail(int AnchorTransactionID, int TransactionTypeID)
        {
            ViewBag.Title = "Detail Cellar";
            ViewBag.Message = "";

            CellarBussines.GetCellarRequest request = new CellarBussines.GetCellarRequest()
            {
                CellarID = AnchorTransactionID,
            };
            TransactionConfigurateBussines.GetTransactionConfigurateRequest requestTransaction = new TransactionConfigurateBussines.GetTransactionConfigurateRequest() {
                AnchorTransactionID = AnchorTransactionID,
                TransactionTypeID = TransactionTypeID
            };

            CellarBussines.GetCellarResponse c = CellarBussines.Select.GetCellar(request);
            int to = TransactionConfigurateBussines.Select.GetTotalAmountToTransaction(requestTransaction).TotalAmount;
            request.ProductID = (int)c.Cellar.idProduct;
            var model = new CellarViewModels()
            {
                Error = c.Error,
                total = to,
                Cellar = c.Cellar,
                Transaction = new Transactions(),
                TransactionList = TransactionConfigurateBussines.Select.GetTransactionList(requestTransaction).TransactionList,
                ProductList = ProductBussines.Select.GetProduct().ProductList,
                CellarAreaList = CellarAreaBussines.Select.GetCellarAreaList().CellarAreaList,
                CellarList = CellarBussines.Select.GetCellarByProductID(request).CellarList,
                ProviderList = ProviderBussines.Select.GetProviderList().ProviderList
            };
            return PartialView(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult CellarCreate()
        {
            ViewBag.Title = "New Cellar";
            ViewBag.Message = "";

            var model = new CellarViewModels()
            {
                ProductList = ProductBussines.Select.GetProduct().ProductList,
                CellarAreaList = CellarAreaBussines.Select.GetCellarAreaList().CellarAreaList,
                ProviderList = ProviderBussines.Select.GetProviderList().ProviderList
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CellarCreate(CellarViewModels data)
        {

            CellarBussines.GetCellarResponse CellarInformation = new CellarBussines.GetCellarResponse()
            {
                Cellar = data.Cellar,
                transaction = data.Transaction
            };
            CellarInformation.Cellar.amount = data.Transaction.amount;
            CellarInformation.Cellar.createDate = data.Transaction.createDate;
            CellarBussines.GetCellarRequest request = new CellarBussines.GetCellarRequest() {
                TransactionTypeID = 1
            };
            CellarBussines.Insert.Cellar(CellarInformation, request);

            return RedirectToAction("CellarView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult CellarUpdate(int AnchorTransactionID, int TransactionTypeID)
        {
            ViewBag.Title = "Update Cellar";
            ViewBag.Message = "";

            CellarBussines.GetCellarRequest request = new CellarBussines.GetCellarRequest()
            {
                CellarID = AnchorTransactionID,
            };
            TransactionConfigurateBussines.GetTransactionConfigurateRequest requestTransaction = new TransactionConfigurateBussines.GetTransactionConfigurateRequest()
            {
                AnchorTransactionID = AnchorTransactionID,
                TransactionTypeID = TransactionTypeID
            };

            CellarBussines.GetCellarResponse c = CellarBussines.Select.GetCellar(request);
            int to = TransactionConfigurateBussines.Select.GetTotalAmountToTransaction(requestTransaction).TotalAmount;
            request.ProductID = (int)c.Cellar.idProduct;
            var model = new CellarViewModels()
            {
                Error = c.Error,
                total = to,
                Cellar = c.Cellar,
                Transaction = new Transactions(),
                TransactionList = TransactionConfigurateBussines.Select.GetTransactionList(requestTransaction).TransactionList,
                ProductList = ProductBussines.Select.GetProduct().ProductList,
                CellarAreaList = CellarAreaBussines.Select.GetCellarAreaList().CellarAreaList,
                CellarList = CellarBussines.Select.GetCellarByProductID(request).CellarList,
                ProviderList = ProviderBussines.Select.GetProviderList().ProviderList
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CellarUpdate(CellarViewModels data)
        {
            CellarBussines.GetCellarRequest request = new CellarBussines.GetCellarRequest() {
                CellarID = data.Cellar.id,
                TransactionTypeID = 1
            };
            CellarBussines.GetCellarResponse cellarInformation = new CellarBussines.GetCellarResponse();
            cellarInformation.transaction = data.Transaction;

            int oldAmount = (int)CellarBussines.Select.GetCellar(request).Cellar.amount;
            int currentAmount = (int)data.Transaction.amount;
            int newAmount = oldAmount + currentAmount;
            request.amount = newAmount;

            cellarInformation.Cellar = new Cellars
            {
                id = data.Cellar.id,
                amount = newAmount
            };            
            CellarBussines.Update.Cellar(cellarInformation, request);
            return RedirectToAction("CellarView", new { successful = true, ResultAction = "All Changes was successful" });
        }
        #endregion



    }
}