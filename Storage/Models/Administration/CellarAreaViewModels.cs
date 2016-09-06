using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bussines;
using Bussines.Handler;

namespace Storage.Models.Administration
{
    public class CellarAreaViewModels
    {
        public int CellarAreaID { get; set; }
        public ErrorObject Error { get; set; }
        public CellarArea CellarArea { get; set; }
        public List<CellarArea> CellarAreaList { get; set; }
    }
}