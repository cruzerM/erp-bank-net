
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Domain.Entities
{
    public enum TransactionType
    {
        Transfer,Payment,Card,Cheque,Withdrawal
    }

    public enum TransactionStatus
    {
        Passed,Hold,Blocked
    }

    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public int? TransmitterAccountId { get; set; }

        public virtual Accounts TransmitterAccount { get; set; }

        public int? BeneficiaryAccountId { get; set; }

        public virtual Accounts BeneficiaryAccount { get; set; }

        
        [Required(ErrorMessage = "Date Required")]
        public DateTime OperationDate { get; set; }

        
        [Required(ErrorMessage = "Date Required")]
        public DateTime ValueDate { get; set; }

        [Required(ErrorMessage = "Amount Required")]
        public float Amount { get; set; }

        [Required(ErrorMessage = "Status Required")]
        public TransactionStatus Status { get; set; }

        [Required(ErrorMessage = "Type Required")]
        public TransactionType Type { get; set; }
    }

}
