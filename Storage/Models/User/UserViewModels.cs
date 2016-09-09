using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bussines.User;
using Bussines.Handler;


namespace Storage.Models.User
{
    public class UserViewModels
    {
        public int CellarAreaID { get; set; }
        public ErrorObject Error { get; set; }
        public Users Users { get; set; }
        public List<Users> UsersList { get; set; }

    }
}