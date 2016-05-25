using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeen.ViewModels
{
    public class AccountingsReceipt
    {
        public int Id { get; set; }
        public decimal StartCash { get; set; }
        public decimal? EndCash { get; set; }
        public decimal CardCash { get; set; }
        public DateTime Timestamp { get; set; }
        public Boolean LockStatus { get; set; }
        public List<Product> StartProduct { get; set; }
        public List<Product> EndProduct { get; set; }
    }
}