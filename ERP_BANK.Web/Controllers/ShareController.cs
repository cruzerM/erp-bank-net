using ERP_BANK.Domain.Entities;
using ERP_BANK.Service;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace ERP_BANK.Web.Controllers
{
    [Authorize]
    public class ShareController : Controller
    {
        IShareService shse = new ShareService();
        IBrokerService dir = new BrokerService();
        public ShareController()
        {

        }
        public ShareController(IShareService shse)
        {
            this.shse = shse;
         }
        // GET: Share
        public ActionResult Index()
        {
            var l = shse.GetAllShares();
           
            return View(l);
        }

        // GET: Share/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Share/Create
        public ActionResult Create()
        {
            ViewBag.idBroker = new SelectList(dir.GetAllBrokers(), "idBroker", "title");

            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(share c)
        {

            if (ModelState.IsValid)
            {
                shse.AddShare(c);
                ViewBag.idBroker = new SelectList(dir.GetAllBrokers(), "idBroker", "title", c.idBroker);
                int idc = c.idshare;
                SendMail(idc);
                return RedirectToAction("Index");
            }
            else
            {
                return View();

            }

        }





        // POST: Share/Create
        //[HttpPost]
        //public ActionResult Create(share s)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        shse.AddShare(s);
        //        return RedirectToAction("Index");
        //    }

        //    else
        //        ViewBag.idBroker = new SelectList(brse.GetAllBrokers(), "idBroker", "title");
        //        return View();

        //}

        // GET: Share/Edit/5
        public ActionResult Edit(int id)
        {
               return View();
        }

        // POST: Share/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            share a = shse.GetById(id);

            a.amountInvested = float.Parse(collection["amountInvested"]);
            a.idBroker = int.Parse(collection["idBroker"]);
           // Convert.ChangeType(collection["broker"], typeof(broker));
            a.currency1 = collection["currency1"];
            a.currency2 = collection["currency2"];
            a.dateOfPurchase = Convert.ToDateTime(collection["dateOfPurchase"]);
            a.dateOfSale= Convert.ToDateTime(collection["dateOfSale"]);
            a.gain= float.Parse(collection["gain"]);

            shse.UpdateShare(a);

            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View(a);
            }
        }

        // GET: Share/Delete/5
        public ActionResult Delete(int id)
        {
            share s = shse.GetById(id);
            shse.DeleteShare(s);
            return RedirectToAction("Index");
        }

        // POST: Share/Delete/5
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



        // GET: Sponsor/SendMail/5
        public ActionResult SendMail(int id)
        {
            share share = shse.GetById(id);
            string mailshare = share.email;
            using (MailMessage mail = new MailMessage("yesmine.slim@gmail.com", mailshare))
            {

                mail.Subject = "Confirmation d achat daction";
                mail.Body = "Hello, you just bought a share from our bank broker, good luck with that!";

                mail.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential("yesmine.slim@gmail.com", "yesmine_slim");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 25;
                //smtp.Send(mail);
                ViewBag.Message = "Sent";
                return RedirectToAction("Index");
            }
        }

    }
}
