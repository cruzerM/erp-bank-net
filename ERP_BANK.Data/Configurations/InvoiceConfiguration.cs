using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Configurations
{
   public class InvoiceConfiguration : EntityTypeConfiguration<invoice>
    {
        public InvoiceConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.IdInvoice);

            // Properties
            this.Property(t => t.InvoiceType)
                .HasMaxLength(45);

            this.Property(t => t.InvoiceOwner)
                .HasMaxLength(45);

            this.Property(t => t.InvoiceDescription)
                .HasMaxLength(255);

            
            // Relationships
            this.HasOptional(t => t.direction)
                .WithMany(t => t.invoices)
                .HasForeignKey(d => d.PK_InvoiceDirection);


        }
    }
}
