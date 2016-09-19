using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private ERPBankContext dataContext;
        public ERPBankContext DataContext { get { return dataContext; } }

        public DatabaseFactory()
        {
            dataContext = new ERPBankContext();
        }
        protected override void DisposeCore()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }
    }

}
