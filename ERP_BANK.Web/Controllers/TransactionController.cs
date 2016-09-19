using ERP_BANK.Domain.Entities;
using ERP_BANK.Service;
using ERP_BANK.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ERP_BANK.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        TransactionService tservice = new TransactionService();
        AccountsService aservice = new AccountsService();


        public void auto()
        {
            var transaction = tservice.GetMany();
            if (transaction != null)
            {
                foreach (var item in transaction)
                {
                    if (item.Status == TransactionStatus.Hold)
                    {
                        item.Status = TransactionStatus.Passed;
                        tservice.Update(item);
                        
                        if (item.BeneficiaryAccountId != null && item.TransmitterAccountId != null)
                        {
                            Accounts BeneficiaryAccount = aservice.GetById(Convert.ToInt64(item.BeneficiaryAccountId));
                            Accounts TransmitterAccount = aservice.GetById(Convert.ToInt64(item.TransmitterAccountId));
                            BeneficiaryAccount.AccountBalance = BeneficiaryAccount.AccountBalance + item.Amount;
                            TransmitterAccount.AccountBalance = TransmitterAccount.AccountBalance - item.Amount;
                            aservice.Update(BeneficiaryAccount);
                            aservice.Update(TransmitterAccount);
                            aservice.Commit();
                        }
                       if (item.BeneficiaryAccountId != null && item.TransmitterAccountId == null)
                        {
                            Accounts BeneficiaryAccount = new Accounts();
                            BeneficiaryAccount = aservice.GetById((long)item.BeneficiaryAccountId);
                            float add = BeneficiaryAccount.AccountBalance + item.Amount;
                            BeneficiaryAccount.AccountBalance = add;
                            aservice.Update(BeneficiaryAccount);
                            aservice.Commit();
                        }
                        if (item.BeneficiaryAccountId == null && item.TransmitterAccountId != null)
                        {
                            Accounts TransmitterAccount = aservice.GetById(Convert.ToInt64(item.TransmitterAccountId));
                            TransmitterAccount.AccountBalance = TransmitterAccount.AccountBalance - item.Amount;
                            aservice.Update(TransmitterAccount);
                            aservice.Commit();
                        }
                    }
                }
                tservice.Commit();
            }
        }

        // GET: Transaction
        public ActionResult Index()
        {
            auto();
            var transaction = tservice.GetMany();
            List<TransactionViewModel> transactionsModel = new List<TransactionViewModel>();
            if (transaction != null)
            {
                foreach(var item in transaction)
                {


                    transactionsModel.Add( new TransactionViewModel
                    {
                      Id = item.Id,
                      TransmitterRIB = item.TransmitterAccountId,
                      BeneficiaryRIB = item.BeneficiaryAccountId,
                      OperationDate = item.OperationDate,
                      ValueDate = item.ValueDate,
                      Amount = item.Amount,
                      Status = item.Status.ToString(),
                      Type = item.Type.ToString()
                    }
                    );
                  }
                return View(transactionsModel);
            }
            return View();
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transaction = tservice.GetById(Convert.ToInt64(id));
            if (transaction == null)
            {
                return HttpNotFound();
            }
            TransactionViewModel transactionModel = new TransactionViewModel
            {
                Id = transaction.Id,
                TransmitterRIB =transaction.TransmitterAccountId,
                BeneficiaryRIB=transaction.BeneficiaryAccountId,
                OperationDate=transaction.OperationDate,
                ValueDate=transaction.ValueDate,
                Amount=transaction.Amount,
                Status=transaction.Status.ToString(),
                Type=transaction.Type.ToString()
            };
            return View(transactionModel);
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction
                {
                    Id=transactionModel.Id,
                    TransmitterAccountId=transactionModel.TransmitterRIB,
                    BeneficiaryAccountId=transactionModel.BeneficiaryRIB,
                    OperationDate=transactionModel.OperationDate,
                    ValueDate=transactionModel.ValueDate,
                    Amount=transactionModel.Amount,
                    Status= (TransactionStatus) Enum.Parse(typeof(TransactionStatus),"Hold",true),
                    Type = (TransactionType)Enum.Parse(typeof(TransactionType),transactionModel.Type,true)
                };
                tservice.Add(transaction);
                tservice.Commit();
                return RedirectToAction("Index");
            }

            return View(transactionModel);
        }

        //GET: Transaction/Account

        public ActionResult Account()
        {
            return View();
        }

        //POST: Transaction/Account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Account(TransactionViewModel transactionModel)
        {
            List<TransactionViewModel> transactionsModel = new List<TransactionViewModel>();
            ViewBag.RIB = transactionModel.AccountRIB;
            var transaction = tservice.GetMany(t=>((t.TransmitterAccountId== transactionModel.AccountRIB || t.BeneficiaryAccountId == transactionModel.AccountRIB) && t.OperationDate >= transactionModel.beginDate && t.OperationDate <= transactionModel.endDate));
            foreach (var item in transaction)
            {
                transactionsModel.Add(
                    new TransactionViewModel
                    {
                        Id = item.Id,
                        TransmitterRIB = item.TransmitterAccountId,
                        BeneficiaryRIB = item.BeneficiaryAccountId,
                        OperationDate = item.OperationDate,
                        ValueDate = item.ValueDate,
                        Amount = item.Amount,
                        Status = item.Status.ToString(),
                        Type = item.Type.ToString()
                    }
                    );
            }
            ViewBag.Transactions = transactionsModel;
            return View();
        }
        // GET: Transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transaction = tservice.GetById(Convert.ToInt64(id));
            if (transaction == null)
            {
                return HttpNotFound();
            }
            TransactionViewModel transactionModel = new TransactionViewModel
            {
                Id = transaction.Id,
                TransmitterRIB = transaction.TransmitterAccountId,
                BeneficiaryRIB = transaction.BeneficiaryAccountId,
                OperationDate = transaction.OperationDate,
                ValueDate = transaction.ValueDate,
                Amount = transaction.Amount,
                Status = transaction.Status.ToString(),
                Type = transaction.Type.ToString()
            };
            return View(transactionModel);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransactionViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction
                {
                    Id = transactionModel.Id,
                    TransmitterAccountId = transactionModel.TransmitterRIB,
                    BeneficiaryAccountId = transactionModel.BeneficiaryRIB,
                    OperationDate = transactionModel.OperationDate,
                    ValueDate = transactionModel.ValueDate,
                    Amount = transactionModel.Amount,
                };
                tservice.Update(transaction);
                tservice.Commit();
                return RedirectToAction("Index");
            }
            return View(transactionModel);
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transaction = tservice.GetById(Convert.ToInt64(id));
            if (transaction == null)
            {
                return HttpNotFound();
            }
            TransactionViewModel transactionModel = new TransactionViewModel
            {
                Id = transaction.Id,
                TransmitterRIB = transaction.TransmitterAccountId,
                BeneficiaryRIB = transaction.BeneficiaryAccountId,
                OperationDate = transaction.OperationDate,
                ValueDate = transaction.ValueDate,
                Amount = transaction.Amount,
                Status = transaction.Status.ToString(),
                Type = transaction.Type.ToString()
            };
            return View(transactionModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var transaction = tservice.GetById(Convert.ToInt64(id));
            tservice.Delete(transaction);
            tservice.Commit();
            return RedirectToAction("Index");
        }
        //GET: Transaction/Block/5
        public ActionResult Block(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transaction = tservice.GetById(Convert.ToInt64(id));
            if (transaction == null)
            {
                return HttpNotFound();
            }
            transaction.Status = TransactionStatus.Blocked;
            tservice.Update(transaction);
            tservice.Commit();
            return RedirectToAction("Index");
        }
    }

}
