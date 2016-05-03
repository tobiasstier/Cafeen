namespace Cafeen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblProduct")]
    public partial class tblProduct
    {
        public int Id { get; set; }

        public int? InventoryId { get; set; }

        public int? CategoryId { get; set; }

        public virtual tblCategory tblCategory { get; set; }

        public virtual tblInventory tblInventory { get; set; }
    }
}
