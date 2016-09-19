using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Configurations
{
    public class TransactionConfiguration:EntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration()
        {
           OnModelCreating(new DbModelBuilder());
        }

        protected void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //one-to-many 
            modelBuilder.Entity<Transaction>()
                        .HasOptional<Accounts>(s => s.TransmitterAccount)
                        .WithMany()
                        .HasForeignKey(s => s.TransmitterAccountId);

            modelBuilder.Entity<Transaction>()
                        .HasOptional<Accounts>(s => s.BeneficiaryAccount)
                        .WithMany()
                        .HasForeignKey(s => s.BeneficiaryAccountId);


        }
    }
}
