using ERP_BANK.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_BANK.Domain.Entities;

namespace ERP_BANK.Data
{
    public class ERPBankContext : DbContext
    {
        public ERPBankContext()
            : base("Name=ERPBank")
        {
            //  Database.SetInitializer<ERPBankContext>(new ERPBankContextInitializer());
        }

        public DbSet<loan> loans { get; set; }
        public DbSet<loan_join_account> loan_join_account { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Accounts> Accountss { get; set; }
        public DbSet<direction> directions { get; set; }
        public DbSet<invoice> invoices { get; set; }
        public DbSet<share> shares { get; set; }
        public DbSet<broker> brokers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //If you want to remove all Convetions and work only with configuration :
            //  modelBuilder.Conventions.Remove<IncludeMetadataConvention>();


            modelBuilder.Configurations.Add(new loanConfiguration());
            modelBuilder.Configurations.Add(new loan_join_accountConfiguration());
            modelBuilder.Configurations.Add(new AccountsConfiguration());
            modelBuilder.Configurations.Add(new TransactionConfiguration());
            modelBuilder.Configurations.Add(new DirectionConfiguration());
            modelBuilder.Configurations.Add(new InvoiceConfiguration());
            modelBuilder.Configurations.Add(new BrokerConfiguration());
            modelBuilder.Configurations.Add(new ShareConfiguration());
        }
    }

    //public class ERPBankContextInitializer : DropCreateDatabaseIfModelChanges<ERPBankContext>
    //{
    //    protected override void Seed(ERPBankContext context)
    //    {
    //        var listLoans = new List<Loan>{
    //            new Loan { Type = "Immobilier" }
    //        };

    //        context.Loans.AddRange(listLoans);
    //        context.SaveChanges();
    //    }
    //}
}
