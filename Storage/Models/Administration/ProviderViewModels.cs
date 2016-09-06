using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bussines;
using Bussines.Handler;


namespace Storage.Models.Administration
{
    public class ProviderViewModels
    {
        public int ProviderID { get; set; }
        public ErrorObject Error { get; set; }
        public Provider Provider { get; set; }
        public List<Provider> ProviderList { get; set; }
    }
}