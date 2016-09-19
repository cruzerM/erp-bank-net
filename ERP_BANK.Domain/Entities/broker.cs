using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_BANK.Domain.Entities
{
    public partial class broker
    {
        public broker()
        {
            this.shares = new List<share>();
        }
        public int idbroker { get; set; }
        [Display(Name = "Default currency")]
        [Required]
        public string defaultCurrency { get; set; }
        [Required]
        [Display(Name = "Min deposit")]
        public Nullable<float> minDeposit { get; set; }
        [Required]
        [Display(Name = "Bonus ( % )")]
        public Nullable<float> bonus { get; set; }
        [Display(Name = "Spred ( % )")]
        [Required]
        public Nullable<float> spred { get; set; }
        [Display(Name = "Payment")]
        [Required]
        public string payment { get; set; }
        [Display(Name = "Title")]
        [Required]
        public string title { get; set; }
        public virtual ICollection<share> shares { get; set; }
    }
}
