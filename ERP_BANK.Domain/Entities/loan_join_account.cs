using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Domain.Entities
{
    public class loan_join_account
    {
        public int id { get; set; }
        [Display(Name = " Loan")]
        public int FK_loan { get; set; }
        [Display(Name = "Account")]
        public int FK_account { get; set; }
        [Display(Name = "Date")]
        public DateTime? date { get; set; }
        public virtual AccountsCurrent account { get; set; }
        public virtual loan loan { get; set; }
    }
}
