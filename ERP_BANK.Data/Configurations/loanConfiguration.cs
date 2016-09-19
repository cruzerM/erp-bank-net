using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Configurations
{
    class loanConfiguration : EntityTypeConfiguration<loan>
    {
        public loanConfiguration()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Type)
                .HasMaxLength(1073741823);

            Property(t => t.Warranty)
                .HasMaxLength(1073741823);



            // Relationships
            HasRequired(t => t.account)
                .WithMany(t => t.Loans)
                .HasForeignKey(d => d.IdAccount);
        }


    }
}
