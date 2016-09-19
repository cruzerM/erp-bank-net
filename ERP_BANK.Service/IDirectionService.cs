
using ERP_BANK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Service
{
   public interface IDirectionService
    {

        void AddDirection(direction d);

        void deleteDirection(direction d);

        void updateDirection(direction d);
        List<direction> getAllDirection();

        direction getdirectionById(int id);

        List<invoice> getInvoicesByIdDrection(int id);

        invoice getInvoiceByIdId( int idinvoice);

        List<invoice> getInvoiceByIdTypeIn(int id);
        List<invoice> getInvoiceByIdTypeOut(int id);




    }
}
