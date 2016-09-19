using ERP_BANK.Data.Infrastructure;
using ERP_BANK.Domain.Entities;
using Service;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Service
{
    public class BrokerService :  IBrokerService
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public void AddBroker(broker b)
        {
            ut.BrokerRepository.Add(b);
            ut.Commit();
        }

        public void DeleteBroker(broker b)
        {
            ut.BrokerRepository.Delete(b);
            ut.Commit();
        }

        public void UpdateBroker(broker b)
        {
            ut.BrokerRepository.Update(b);
            ut.Commit();
        }

        public List<broker> GetAllBrokers()
        {
            return ut.BrokerRepository.GetAll().ToList();
        }

        public IEnumerable<broker> GetBrokersByName(string title)
        {
            return ut.BrokerRepository.GetMany(t => t.title.Contains(title));
        }

        public broker GetById(int idBroker)
        {
            return ut.BrokerRepository.GetById(idBroker);
        }

    }
      
}