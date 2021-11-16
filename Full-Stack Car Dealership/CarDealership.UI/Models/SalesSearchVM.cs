using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class SalesSearchVM
    {
        public IEnumerable<SelectListItem> Users { get; set; }

    }
}