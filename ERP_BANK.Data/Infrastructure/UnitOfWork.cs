using ERP_BANK.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
       
         private ERPBankContext dataContext;

        IDatabaseFactory dbFactory;
        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
            dataContext = dbFactory.DataContext;
        }
        


        public void Commit()
        {
            dataContext.SaveChanges();
        }
         public void CommitAsync()
         {
             dataContext.SaveChangesAsync();
         }
        public void Dispose()
        {
            dataContext.Dispose();
        }
        public IRepositoryBaseAsync<T> getRepository<T>() where T : class
        {
            IRepositoryBaseAsync<T> repo = new RepositoryBase<T>(dbFactory);
            return repo;
        }

        private IInvoiceRepository invoiceRepository;

        public IInvoiceRepository InvoiceRepository
        {
            get { return invoiceRepository = new InvoiceRepository(dbFactory); }
        }

        private IDirectionRepository directionRepository;
        public IDirectionRepository DirectionRepository
        {
            get { return directionRepository = new DirectionRepository(dbFactory); ; }
        }

        private IShareRepository shareRepository;
        public IShareRepository ShareRepository
        {
            get
            {
                return shareRepository = new ShareRepository(dbFactory); ;
            }
        }
        
        private IBrokerRepository brokerRepository;
        public IBrokerRepository BrokerRepository
        {
            get
            {
                return brokerRepository = new BrokerRepository(dbFactory); ;
            }
        }

    }
}
