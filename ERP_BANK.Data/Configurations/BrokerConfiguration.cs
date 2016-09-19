using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Configurations
{
    public class BrokerConfiguration : EntityTypeConfiguration<broker>
    {
        public BrokerConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.idbroker);

            // Properties
            this.Property(t => t.defaultCurrency)
                .HasMaxLength(45);

            this.Property(t => t.payment)
                .HasMaxLength(45);

            this.Property(t => t.title)
                .HasMaxLength(45);

            // Table & Column Mappings
            //this.ToTable("broker", "erp_bank");
            //this.Property(t => t.idbroker).HasColumnName("idbroker");
            //this.Property(t => t.defaultCurrency).HasColumnName("defaultCurrency");
            //this.Property(t => t.minDeposit).HasColumnName("minDeposit");
            //this.Property(t => t.bonus).HasColumnName("bonus");
            //this.Property(t => t.spred).HasColumnName("spred");
            //this.Property(t => t.payment).HasColumnName("payment");
            //this.Property(t => t.title).HasColumnName("title");
        }
    }
}
