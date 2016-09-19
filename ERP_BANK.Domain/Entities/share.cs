using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_BANK.Domain.Entities
{
    public partial class share
    {
        public int idshare { get; set; }
        [Display(Name = "From currency")]
        [Required]
        public string currency1 { get; set; }
        [Display(Name = "To currency")]
        [Required]
        public string currency2 { get; set; }
        [Display(Name = "Amount invested")]
        [Required]
        public Nullable<float> amountInvested { get; set; }
        [Display(Name = "Date of purchase")]
        public Nullable<System.DateTime> dateOfPurchase { get; set; }
        [Display(Name = "Date of sale")]
        public Nullable<System.DateTime> dateOfSale { get; set; }
        [Display(Name = "Gain ( % )")]
        [Required]
        public Nullable<float> gain { get; set; }
        [Display(Name = "Trader mail address")]
        [Required]
        public string email { get; set; }

        [Display(Name = "Broker")]
        public int idBroker { get; set; }
        public virtual broker broker { get; set; }
    }
}
