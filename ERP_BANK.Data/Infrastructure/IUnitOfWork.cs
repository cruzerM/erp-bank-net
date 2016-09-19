using ERP_BANK.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ERP_BANK.Data.Repositories;

namespace ERP_BANK.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBaseAsync<T> getRepository<T>() where T : class;
        IDirectionRepository DirectionRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IShareRepository ShareRepository { get; }
        IBrokerRepository BrokerRepository { get; }
        void CommitAsync();
        void Commit();
       
    }

}
