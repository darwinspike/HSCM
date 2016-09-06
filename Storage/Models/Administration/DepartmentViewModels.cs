using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Bussines;
using Bussines.Handler;
namespace Storage.Models.Administration
{
    public class DepartmentViewModels
    {
        public int DepartmentID { get; set; }
        public ErrorObject Error { get; set; }
        public Department Department { get; set; }
        public List<Department> DepartmentList { get; set; }

    }
}