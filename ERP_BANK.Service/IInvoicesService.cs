
using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Service
{
    public interface IInvoicesService
    {


        void AddInvoice(invoice c);

        void deleteInvoice(invoice c);

        void updateInvoice(invoice c);
        invoice getInvoiceById(int id);

        //   List<invoice> getInvoiceBydirection(string type);

        List<invoice> getInvoiceIn();
        List<invoice> getInvoiceOut();
        
        List<invoice> getByOwner(String type);

        int countFactIn();
        int countFactOut();
        


        List<invoice> getAllInvoices();
       /* List<invoice> getInvoiceByDirectionName(String NameDirection);*/




    }
}
