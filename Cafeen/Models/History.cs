using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafeen.Models
{
    [Table("tblHistory")]
    public class History
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public string CatName { get; set; }
        public decimal CatPrice { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
    }
}