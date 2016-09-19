
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
    public class DirectionController : Controller

    {
        IDirectionService ause = new DirectionService();

        public DirectionController()
        {
            


        }
        // GET: Direction
        public ActionResult Index()
        {
            var l = ause.getAllDirection();
            return View(l);
        }

        // GET: Direction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Direction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Direction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( direction d)
        {
           
            
            if (ModelState.IsValid)
              {
                  ause.AddDirection(d);
                  return RedirectToAction("Index");
              }
              else
              {
                  return View();
              }

          


        }

        // GET: Direction/Edit/5
        public ActionResult Edit(int id)
        {
            direction a = ause.getdirectionById(id);
            ause.updateDirection(a);
            return View(a);
        }

        // POST: Direction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            direction a = ause.getdirectionById(id);
            a.DirectionName = collection["DirectionName"];
            a.DirectionType = collection["DirectionType"];
            a.DirectionBudget = collection["DirectionBudget"];

            ause.updateDirection(a);

            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View(a);
            }
        }

        // GET: Direction/Delete/5
        public ActionResult Delete(int id)
        {
            direction b = ause.getdirectionById(id);
            ause.deleteDirection(b);
            return RedirectToAction("Index");
        }

        // POST: Direction/Delete/5
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

          //Direction/Index2/1
        //-----------------------------------
      //  [HttpPost]
        public ActionResult AllInvoice(int id)
        {
            var l = ause.getInvoicesByIdDrection(id);
            return View(l);
        }


        public ActionResult View(int id)
        {

            var l = ause.getInvoiceByIdId(id);
            return View(l);


        }


        public ActionResult InvIn(int id)
        {
            var a = ause.getInvoiceByIdTypeIn(id);
            return View(a);

        }


        public ActionResult InvOut(int id)
        {
            var a = ause.getInvoiceByIdTypeOut(id);
            return View(a);

        }


    }
}
