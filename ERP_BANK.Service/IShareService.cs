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
    public interface IShareService 
    {
        void AddShare(share s);
        void DeleteShare(share b);
        void UpdateShare(share b);
        List<share> GetAllShares();
        share GetById(int id);
    }
}
