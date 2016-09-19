using ERP_BANK.Data.Infrastructure;
using ERP_BANK.Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Service
{
    public class TransactionService : Service<Transaction> , ITransactionService
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);
        public TransactionService() : base(ut){ }
    }
}
