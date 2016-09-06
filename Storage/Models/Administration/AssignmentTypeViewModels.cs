using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bussines;
using Bussines.Handler;

namespace Storage.Models.Administration
{
    public class AssignmentTypeViewModels
    {

        public int DepartmentID { get; set; }
        public ErrorObject Error { get; set; }
        public AssignmentType AssignmentType { get; set; }
        public List<AssignmentType> AssignmentTypeList { get; set; }
    }
}