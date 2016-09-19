using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ERP_BANK.Service;
using ERP_BANK.Domain.Entities;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ERP_BANK.Web.Controllers
{
    public class InvoiceWebApiController : ApiController
    {
        IInvoicesService invoiceService = new InvoicesSerivce();

        // GET: api/InvoiceWebApi
        public IEnumerable<invoice> Get()
        {

            List<invoice> listInvoices = invoiceService.getAllInvoices();
            
            foreach (invoice i in listInvoices)
            {
                if(i.ItemsString!= null)
                    i.Items = i.ItemsString.Split(',').Select(p => p.Trim().Split(':')).ToDictionary(p => p[0], p => int.Parse(p[1]));
            }
            


            return listInvoices.Where(t => t.StatusInventory == false);
        }

        // GET: api/InvoiceWebApi/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST: api/InvoiceWebApi
        //public void Post([FromBody]string value)
        //{
        //}

        // POST: api/InvoiceWebApi
        public void Post(invoice inv)
        {
            inv.InvoiceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            inv.InvoiceType = "Out";
            inv.PK_InvoiceDirection = 1;

            inv.ItemsString = string.Join(", ", inv.Items.Select(m => m.Key + ":" + m.Value).ToArray());

            invoiceService.AddInvoice(inv);

        }
        // PUT: api/InvoiceWebApi/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        public void Put(int id)
        {


            if (id != 0)
            {
                invoice i = invoiceService.getInvoiceById(id);
                if (i != null)
                {
                    i.StatusInventory = true;
                    invoiceService.updateInvoice(i);
                }

            }

        }
        // DELETE: api/InvoiceWebApi/5
        public void Delete(int id)
        {
        }
    }
}
