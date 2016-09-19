using ERP_BANK.Data.Infrastructure;
using ERP_BANK.Domain.Entities;
using ERP_BANK.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERP_BANK.Service
{
    public class InvoicesSerivce : IInvoicesService
    {
        static public DatabaseFactory dbFactory = new DatabaseFactory();
        UnitOfWork utwk = new UnitOfWork(dbFactory);
  

        public void AddInvoice(invoice c)
        {
            utwk.InvoiceRepository.Add(c);
            utwk.Commit();
        }

        public List<invoice> getByOwner(String type)
        {
            String t = type;
         
           return utwk.InvoiceRepository.GetAll().Where(i => i.InvoiceOwner==type).ToList();
        }


        public void deleteInvoice(invoice c)
        {
            utwk.InvoiceRepository.Delete(c);
            utwk.Commit();
        }

        public List<invoice> getAllInvoices()
        {
            return utwk.InvoiceRepository.GetAll().ToList();
        }

        /*public List<invoice> getInvoiceByDirectionName(String nameDirection)
        {
            var tab = new List<Table>();
            var lr = mycontext.Reservations.Where(r => r.DateReservation == DateTime.Now).ToList();
            foreach (Reservation item in lr)
            { tab.Add(item.matable); }
            return tab;


            return utwk.InvoiceRepository.GetAll().ToList();
        }*/

        public invoice getInvoiceById(int i)
        {
           return utwk.InvoiceRepository.GetById(i);
           
        }

        public void updateInvoice(invoice c)
        {
            utwk.InvoiceRepository.Update(c);
            utwk.Commit();
        }





        public List<invoice> getInvoiceIn() {
           

            return utwk.InvoiceRepository.GetAll().Where(i => i.InvoiceType == "In").ToList();


        }
        public List<invoice> getInvoiceOut() {
            return utwk.InvoiceRepository.GetAll().Where(i => i.InvoiceType == "Out").ToList();


        }



        public int countFactIn() {
            return utwk.InvoiceRepository.GetAll().Where(i => i.InvoiceType == "In").Count();
          
          

        }
        public int countFactOut() {
            return utwk.InvoiceRepository.GetAll().Where(i => i.InvoiceType == "In").Count();
        }


       


    }
}
