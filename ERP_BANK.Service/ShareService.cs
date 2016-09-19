using ERP_BANK.Data.Infrastructure;
using ERP_BANK.Domain.Entities;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Service
{
    public class ShareService :  IShareService
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        //public ShareService():base(ut)
        //{

        //}
        public void AddShare(share s)
        {
            ut.ShareRepository.Add(s);
            ut.Commit();
        }

        public void DeleteShare(share s)
        {
            ut.ShareRepository.Delete(s);
            ut.Commit();
        }

        public void UpdateShare(share s)
        {
            ut.ShareRepository.Update(s);
            ut.Commit();
        }

        public List<share> GetAllShares()
        {
            return ut.ShareRepository.GetAll().ToList();
        }
      
        public share GetById(int idShare)
        {
            return ut.ShareRepository.GetById(idShare);
        }

     
    }
}
