using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Configurations
{
    class loan_join_accountConfiguration : EntityTypeConfiguration<loan_join_account>
    {
        public loan_join_accountConfiguration()
        {
            // Primary Key
            HasKey(t => t.id);


            // Relationships
            HasRequired(t => t.account)
                .WithMany(t => t.loan_join_account)
                .HasForeignKey(d => d.FK_account);
            HasRequired(t => t.loan)
                .WithMany(t => t.loan_join_account)
                .HasForeignKey(d => d.FK_loan);
        }
    }
}
