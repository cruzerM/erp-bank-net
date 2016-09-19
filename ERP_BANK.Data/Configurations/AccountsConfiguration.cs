using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Configurations
{
    public class AccountsConfiguration : EntityTypeConfiguration<Accounts>
    {
        public AccountsConfiguration()
        {
            Map<AccountsCurrent>(c =>
            {
                c.Requires("TypeAccount").HasValue("Current_Account");
            });
            Map<AccountsSaving>(c =>
            {
                c.Requires("TypeAccount").HasValue("Saving_Account");
            });
            Map<AccountsBlocked>(c =>
            {
                c.Requires("TypeAccount").HasValue("Blocked_Account");
            });

            //One To Many
            HasOptional(p => p.Direction)   // 0,1..*   //if you need 1..* use HasRequired()
                .WithMany(c => c.Accounts)
                .HasForeignKey(p => p.DirectionId)
                .WillCascadeOnDelete(false);

        }

    }
}
