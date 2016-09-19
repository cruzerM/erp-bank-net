using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Domain.Entities
{
    public class direction
    {
        [Key]
        public int idDirection { get; set; }
        public string DirectionName { get; set; }
        public string DirectionType { get; set; }
        public string DirectionBudget { get; set; }
        virtual public ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<invoice> invoices { get; set; }


        public direction()
        {
            this.invoices = new List<invoice>();
        }

    }
}
