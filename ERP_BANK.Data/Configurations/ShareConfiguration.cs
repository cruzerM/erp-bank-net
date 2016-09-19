using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Configurations
{
    public class ShareConfiguration : EntityTypeConfiguration<share>
    {
        public ShareConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.idshare);

            // Properties
            this.Property(t => t.currency1)
                .HasMaxLength(45);

            this.Property(t => t.currency2)
                .HasMaxLength(45);

            this.Property(t => t.email)
                .HasMaxLength(45);

            // Table & Column Mappings
            //this.ToTable("share", "erp_bank");
            //this.Property(t => t.idshare).HasColumnName("idshare");
            //this.Property(t => t.currency1).HasColumnName("currency1");
            //this.Property(t => t.currency2).HasColumnName("currency2");
            //this.Property(t => t.amountInvested).HasColumnName("amountInvested");
            //this.Property(t => t.dateOfPurchase).HasColumnName("dateOfPurchase");
            //this.Property(t => t.dateOfSale).HasColumnName("dateOfSale");
            //this.Property(t => t.gain).HasColumnName("gain");
            //this.Property(t => t.idBroker).HasColumnName("idBroker");
            //this.Property(t => t.email).HasColumnName("email");

            // Relationships
            this.HasRequired(t => t.broker)
                .WithMany(t => t.shares)
                .HasForeignKey(d => d.idBroker);
        }
    }
}
