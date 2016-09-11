using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Bussines.Handler;
using Bussines.Cellar;
using Bussines.Product;
using Bussines;
using Bussines.Transaction;

namespace Storage.Models.Cellar
{
    public class CellarViewModels
    {
        public int ID { get; set; }
        public ErrorObject Error { get; set; }
        public int total { get; set; }

        public Cellars Cellar { get; set; }
        public List<Cellars> CellarList { get; set; }

        public Products Product { get; set; }
        public List<Products> ProductList { get; set; }
        public ProductType ProductType { get; set; }
        public List<ProductType> ProductTypeList { get; set; }

        public Transactions Transaction { get; set; }
        public List<Transactions> TransactionList { get; set; }

        public Provider Provider { get; set; }
        public List<Provider> ProviderList { get; set; }

        public CellarArea CellarArea { get; set; }
        public List<CellarArea> CellarAreaList { get; set; }

    }
}