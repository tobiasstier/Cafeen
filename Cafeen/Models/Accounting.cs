﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafeen.Models
{
    [Table("tblAccounting")]
    public class Accounting
    {
        public int Id { get; set; }
        public decimal StartCash { get; set; }
        public decimal? EndCash { get; set; }
        public decimal CardCash { get; set; }
        public DateTime Timestamp { get; set; }
        public Boolean LockStatus { get; set; }
        public string StartProduct { get; set; }
        public string EndProduct { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}