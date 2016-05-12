using Cafeen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeen.ViewModels
{
    public class tblProductsIndexData
    {
        IEnumerable<tblCategory> tblCategories { get; set; }
        IEnumerable<tblProduct> tblProducts { get; set; }
    }
}