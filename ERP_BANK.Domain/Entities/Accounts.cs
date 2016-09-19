using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Domain.Entities
{
    public class Accounts
    {

        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FNameClient { get; set; }

        [Display(Name = "Last Name")]
        public string LNameClient { get; set; }

        [Display(Name = "Cin")]
        public string CinClient { get; set; }

        [Display(Name = "Balance")]
        public float AccountBalance { get; set; }

        public string Libelle { get; set; }
        public string Currency { get; set; }
        public string Note { get; set; }

        [Display(Name = "Salary")]
        public float salary { get; set; }

        [Display(Name = "Type Of Account")]
        public String Type { get; set; }

        //foreign Key properties
        public int? DirectionId { get; set; }

        //navigation properties
        //[ForeignKey("CategoryId ")]      //useless in ths case   
        public virtual direction Direction { get; set; }

        public Accounts()
        {}

    }
}
