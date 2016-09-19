using ERP_BANK.Domain.Entities;
using ERP_BANK.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace ERP_BANK.Web.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        ILoanServices loanService;
        private static Timer aTimer;

        public LoanController()
        {
            this.loanService = new LoanServices();
        }
        public LoanController(ILoanServices loanService)
        {
            this.loanService = loanService;
        }
        // GET: Loan
        public ActionResult Index()
        {
            var loans = loanService.GetLoans();
            return View(loans);
        }

        [HttpPost]
        public ActionResult Index(string SearchString)
        {
            var loans = loanService.GetLoans();
            if (!String.IsNullOrEmpty(SearchString))
            {
                loans = loans.Where(l => l.account.Libelle.Contains(SearchString));
            }
            return View(loans);
        }

        // GET: Loan/Details/5
        public ActionResult Details(int id)
        {
            var loan = loanService.GetById(id);
            return View(loan);
        }

        // GET: Loan
        public ActionResult PayBackLog(int id)
        {
            var log = loanService.GetPayBackLogByLoan(id);
            ViewBag.LoanId = id;

            return View(log);
        }

        // GET: Loan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Loan/Create
        [HttpPost]
        public ActionResult Create(loan loan)
        {
            if (ModelState.IsValid)
            {
                AccountsCurrent account = loanService.GetAccount(loan.account.Libelle);
                Debug.WriteLine("llll : " + account.Id);
                loan.IsPayed = false;
                loan.MonthlyPayment = 0;
                loan.AmountWithInterest = 0;
                loan.RestAmount = 0;

                loanService.AddLoanToAccount(loan, account);

                return RedirectToAction("Index");
            }

            else
            {
                return View();
            }
        }

        public ActionResult CalculateAmountWithInterest(int id)
        {
            var loan = loanService.GetById(id);
            loanService.CalculateAmountWithInterest(loan);
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult CalculateMonthlyPayment(int id)
        {
            var loan = loanService.GetById(id);
            loanService.CalculateMonthlyPayment(loan);
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult PayBack(int id)
        {
            SetTimer(id);


            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();

            Console.WriteLine("Terminating the application...");

            return RedirectToAction("Index");

        }

        private static void SetTimer(int id)
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += delegate (object source, ElapsedEventArgs args) { OnTimedEvent(source, args, id); }; ;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e, int id)
        {
            ILoanServices MyService = new LoanServices();
            loan loan = MyService.GetById(id);

            MyService.PayBackLoan(loan);

            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            Console.WriteLine("Rest amount: " + loan.RestAmount);
            Console.WriteLine("Balance: " + loan.account.AccountBalance);

        }

        // GET: Loan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Loan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Loan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Loan/Delete/5
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
