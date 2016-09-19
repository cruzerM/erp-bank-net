using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Configurations
{
   public class DirectionConfiguration : EntityTypeConfiguration<direction>
    {

        public DirectionConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.idDirection);

            // Properties
            this.Property(t => t.DirectionName)
                .HasMaxLength(45);

            this.Property(t => t.DirectionType)
                .HasMaxLength(45);

            this.Property(t => t.DirectionBudget)
                .HasMaxLength(45);

            
        }


    }
}
