
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.Xml;
using ERP_BANK.Service;
using ERP_BANK.Domain.Entities;
using Rotativa;

namespace GUI.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        IInvoicesService ause=new InvoicesSerivce() ;
        IDirectionService dir=new DirectionService ();
      
        public InvoiceController()
        {
           
        }


        // GET: Invoice
        public ActionResult Index()
        {
            var l = ause.getAllInvoices();
            return View(l);
        }


        [HttpPost]
        public ActionResult Index(String SearchString)
        { String ch = SearchString.ToUpper();


            var l = ause.getAllInvoices();

           
            if (!String.IsNullOrEmpty(SearchString))
            {
                
                l = l.Where(i => i.InvoiceOwner.ToUpper().Contains(ch)).ToList();

            }
            return View(l);
        }





        // GET: Invoice/Details/5
        public ActionResult View(int id)
        {

            var l = ause.getInvoiceById(id);
            return View(l);


        }

        // GET: Invoice/Create
       
        public ActionResult Create()
        {
            ViewBag.PK_InvoiceDirection = new SelectList(dir.getAllDirection(), "idDirection", "DirectionType");
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(invoice c)
        {

            if (ModelState.IsValid)
            {
                ause.AddInvoice(c);
                ViewBag.PK_InvoiceDirection = new SelectList(dir.getAllDirection(), "idDirection", "DirectionType", c.PK_InvoiceDirection);
                return RedirectToAction("Index");
            }
           else
            { 
            return View();

            }

        }

        // GET: Invoice/Edit/5
        
        public ActionResult Edit(int id)
        { 
            invoice a = ause.getInvoiceById(id);
            ause.updateInvoice(a);
            return View(a);




        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            invoice a = ause.getInvoiceById(id);
            
            a.InvoiceDescription = collection["InvoiceDescription"];
            a.InvoiceOwner = collection["InvoiceOwner"];
            a.InvoicePrice= Convert.ToDouble(collection["InvoicePrice"]);
           
            a.InvoiceType=collection["InvoiceType"];
            ause.updateInvoice(a);

            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View(a);
            }
            

           

        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {

            invoice b = ause.getInvoiceById(id);
            ause.deleteInvoice(b);
            return RedirectToAction("Index");
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult InvIn()
        {
            var a = ause.getInvoiceIn();
            return View(a);
           
        }

        public ActionResult InvOut()
        {
            var l = ause.getInvoiceOut();
            return View(l);
        }


        public ActionResult ExportPdf()
        {
            return new ActionAsPdf("View")

            {
                FileName = Server.MapPath("~/Content/Invoicepdf.pdf")

            };

        }


        public ActionResult ExportPdf2(int id)
        {
            return new ActionAsPdf("View/" + id)

            {
                FileName = Server.MapPath("~/Content/Invoicepdf.pdf")

            };

        }



    }
}
