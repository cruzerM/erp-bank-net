using ERP_BANK.Data.Infrastructure;
using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  

namespace ERP_BANK.Data.Repositories
{
    public class BrokerRepository : RepositoryBase<broker>, IBrokerRepository
    {
        public BrokerRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
    public interface IBrokerRepository : IRepositoryBase<broker>
    {

    }
}