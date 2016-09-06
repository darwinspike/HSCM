using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bussines.Product;
using Bussines.Handler;

namespace Storage.Models.Product
{
    public class ProductTypeViewModels
    {
        public int ProductTypeID { get; set; }
        public ErrorObject Error { get; set; }
        public ProductType ProductType { get; set; }
        public List<ProductType> ProductTypeList { get; set; }

    }
}