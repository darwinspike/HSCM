using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Bussines.User;
using Bussines.Handler;
using Bussines;

namespace Storage.Models.User
{
    public class UserConfigurationViewModels
    {
        public int ID { get; set; }
        public ErrorObject Error { get; set; }
        public UserConfiguration UserConfiguration { get; set; }
        public List<UserConfiguration> UserConfigurationList { get; set; }

        public bool ExistUserConfiguration { get; set; }
        public List<Users> User { get; set; }
        public List<TypePermission> TypePermission { get; set; }
        public List<CellarArea> CellarArea { get; set; }
        public List<AssignmentType> AssignmentType { get; set; }
        public List<Category> Category { get; set; }
        public List<Department> Department { get; set; }
    }
}