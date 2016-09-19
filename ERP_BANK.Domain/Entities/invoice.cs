using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_BANK.Domain.Entities
{
    public class invoice
    {
        public int IdInvoice { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceOwner { get; set; }
        public string InvoiceDescription { get; set; }
        public Nullable<double> InvoicePrice { get; set; }


        #region Inventory Attributes
        public bool? StatusInventory { get; set; }
        public int? Quantity { get; set; }
        public String Product { get; set; }

        public String ItemsString { get; set; }

        [NotMapped]
        public Dictionary<String,int> Items { get; set; }

        #endregion
        public Nullable<int> PK_InvoiceDirection { get; set; }

        [JsonIgnore]
        public virtual direction direction { get; set; }
    }
}
