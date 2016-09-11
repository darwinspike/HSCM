using Bussines.FileManager;
using Bussines.Handler;
using Bussines.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Models.Product
{
    public class ProductViewModels
    {
        public int id { get; set; }
        public ErrorObject Error { get; set; }

        public Products Products { get; set; }
        public List<Products> ProductsList { get; set; }

        public ProductType ProductType { get; set; }
        public List<ProductType> ProductTypeList { get; set; }

        public FileManagers FileManagers { get; set; }
        public List<FileManagers> FileManagersList { get; set; }


    }
}