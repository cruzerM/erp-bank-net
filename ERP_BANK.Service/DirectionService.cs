using ERP_BANK.Data.Infrastructure;
using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ERP_BANK.Service
{
    public class DirectionService : IDirectionService
    {
        static public DatabaseFactory dbFactory = new DatabaseFactory();
        UnitOfWork utwk = new UnitOfWork(dbFactory);
      
        public void AddDirection(direction d)
        { 
            utwk.DirectionRepository.Add(d);
            utwk.Commit();
        }

        public void deleteDirection(direction d)
        {
            utwk.DirectionRepository.Delete(d);
        }

        public List<direction> getAllDirection()
        {
            return utwk.DirectionRepository.GetAll().ToList();
        }

        public void updateDirection(direction d)
        {
            utwk.DirectionRepository.Update(d);
        }



        public direction getdirectionById(int id)
        { return utwk.DirectionRepository.GetById(id);
        }


       public List<invoice> getInvoicesByIdDrection(int id)
        {
           
            return utwk.InvoiceRepository.GetAll().Where(i => i.PK_InvoiceDirection == id).ToList();


        }

        public invoice getInvoiceByIdId(int idinvoice)
        {
            return utwk.InvoiceRepository.GetById(idinvoice);


        }


        public List<invoice> getInvoiceByIdTypeIn(int id) {

            return utwk.InvoiceRepository.GetAll().Where(i => i.PK_InvoiceDirection == id)
                                                  .Where(i => i.InvoiceType == "In").ToList();

        }



       public List<invoice> getInvoiceByIdTypeOut(int id) {
            return utwk.InvoiceRepository.GetAll().Where(i => i.PK_InvoiceDirection == id)
                                                  .Where(i => i.InvoiceType == "Out").ToList();
        }







    }
}
