using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Domain.Entities
{
    public class AccountsBlocked : Accounts
    {
        public DateTime? DateDeblocked { get; set; }

        public bool? Blocked { get; set; }
        public float? Benefit { get; set; }
        public AccountsBlocked()
        {}
    }
}
