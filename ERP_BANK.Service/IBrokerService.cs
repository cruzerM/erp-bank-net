using ERP_BANK.Data.Infrastructure;
using ERP_BANK.Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Service
{
    public interface IBrokerService 
    {
        void AddBroker(broker b);
        void DeleteBroker(broker b);
        void UpdateBroker(broker b);
        List<broker> GetAllBrokers();
        IEnumerable<broker> GetBrokersByName(string title);
        broker GetById(int id);
    }
}
