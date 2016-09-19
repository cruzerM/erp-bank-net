using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Domain.Entities
{
    public class loan
    {
        public loan()
        {
            this.loan_join_account = new List<loan_join_account>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        [Range(0, double.MaxValue)]
        public double Amount { get; set; }
        [Range(0, int.MaxValue)]
        public int Period { get; set; }
        [Display(Name = "Monthly Payment")]
        [Range(0, double.MaxValue)]
        public double? MonthlyPayment { get; set; }
        public string Warranty { get; set; }
        [Display(Name = "Interest Rate")]
        public decimal InterestRate { get; set; }
        [Display(Name = "Amount With Interest")]
        [Range(0, double.MaxValue)]
        public double? AmountWithInterest { get; set; }
        [Display(Name = "Rest Amount")]
        [Range(0, double.MaxValue)]
        public double? RestAmount { get; set; }
        [Display(Name = "Account")]
        public int IdAccount { get; set; }
        [Display(Name = "Payed")]
        public bool? IsPayed { get; set; }
        public virtual AccountsCurrent account { get; set; }
        public virtual ICollection<loan_join_account> loan_join_account { get; set; }
    }
}
