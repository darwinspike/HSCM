using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Bussines.Employee;
using Bussines.User;
using Bussines.Handler;

namespace Storage.Models.Employee
{
    public class EmployeeViewModels
    {
        public int ID { get; set; }
        public ErrorObject Error { get; set; }
        public Employees Employee { get; set; }
        public List<Employees> EmployeeList { get; set; }

        public string UserName { get; set; }
        public List<Users> User { get; set; }
        public string UserTypeName { get; set; }
        public List<UserType> UserType { get; set; }



    }
}