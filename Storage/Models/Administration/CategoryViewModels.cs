using Bussines;
using Bussines.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Models.Administration
{
    public class CategoryViewModels
    {

        public int CategoryID { get; set; }
        public bool IfFather { get; set; }
        public int action { get; set; }
        public bool containerChild { get; set; }
        public ErrorObject Error { get; set; }
        public Category Category { get; set; }
        public List<Category> CategoryList { get; set; }

    }
}