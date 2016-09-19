using ERP_BANK.Domain.Entities;
using ERP_BANK.Service;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ERP_BANK.Web.Controllers
{
    [Authorize]
    public class BrokerController : Controller
    {
        IBrokerService brse = new BrokerService();
        public BrokerController()
        {
                
        }
        public BrokerController(IBrokerService brse)
        {
            this.brse = brse;
        }
        public ActionResult Index()
        {
            //System.Threading.Thread.Sleep(4000);
            var l = brse.GetAllBrokers();
            return View(l.ToList());
            
        }

        //recherche
        [HttpPost] //lit les données depuis la vue
        public ActionResult Index(string search)
        {
            var l = brse.GetBrokersByName(search);
            return View(l);
        }

        // GET: Broker/Details/5
        public ActionResult Details(int id)
        {
            var broker = new broker { idbroker = id, title = "HelloBroker"};
            return View(broker);
        }

        // GET: Broker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Broker/Create

        //[HttpPost]


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( broker d)
        {


            if (ModelState.IsValid)
            {
                brse.AddBroker(d);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
             }






        //public ActionResult Create(broker b)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        brse.AddBroker(b);
        //        return RedirectToAction("Index");
        //    }

        //    else

        //        return View();

        //}

        // GET: Broker/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Broker/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            broker a = brse.GetById(id);

            a.bonus = float.Parse(collection["bonus"]);
            a.defaultCurrency = collection["defaultCurrency"];
            a.minDeposit = float.Parse(collection["minDeposit"]);
            a.payment = collection["payment"];
            a.spred = float.Parse(collection["spred"]);

            a.title = collection["title"];
            brse.UpdateBroker(a);

            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View(a);
            }
        }

        // GET: Broker/Delete/5
        public ActionResult Delete(int id)
        {
            broker b = brse.GetById(id);
            brse.DeleteBroker(b);
            return RedirectToAction("Index");

        }

        // POST: Broker/Delete/5
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
    }
}
