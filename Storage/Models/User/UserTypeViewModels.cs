using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Bussines.User;
using Bussines.Handler;


namespace Storage.Models.User
{
    public class UserTypeViewModels
    {
        public int UserTypeID { get; set; }
        public ErrorObject Error { get; set; }
        public UserType UserType { get; set; }
        public List<UserType> UserTypeList { get; set; }
    }
}