using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_BANK.Web.Models
{
    public class TransactionViewModel
    {
        [DisplayName("Transaction ID")]
        public int Id { get; set; }
        [DisplayName("Transmitter RIB")]
        public int? TransmitterRIB { get; set; }
        [DisplayName("Beneficiary RIB")]
        public int? BeneficiaryRIB { get; set; }

        [DisplayName("Account RIB")]
        [Required(ErrorMessage = "Account RIB Required")]
        public int AccountRIB { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Begin date")]
        [Required(ErrorMessage = "Begin date Required")]
        public DateTime beginDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("End date")]
        [Required(ErrorMessage = "End date Required")]
        public DateTime endDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Operation date")]
        [Required(ErrorMessage = "Operation date Required")]
        public DateTime OperationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Value date")]
        [Required(ErrorMessage = "Value date Required")]
        public DateTime ValueDate { get; set; }

        [Required(ErrorMessage = "Amount Required")]
        public float Amount { get; set; }

        
        public string Status { get; set; }

        
        public string Type { get; set; }

    }
}