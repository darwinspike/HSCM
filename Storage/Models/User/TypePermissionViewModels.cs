using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Bussines.User;
using Bussines.Handler;

namespace Storage.Models.User
{
    public class TypePermissionViewModels
    {



        public int TypePermissionID { get; set; }
        public ErrorObject Error { get; set; }
        public TypePermission TypePermission { get; set; }
        public List<TypePermission> TypePermissionList { get; set; }


    }
}