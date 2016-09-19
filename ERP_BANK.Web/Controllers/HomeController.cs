using ERP_BANK.Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_BANK.Service;

namespace ERP_BANK.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IAccountsService serviceAccounts = new AccountsService();
        ITransactionService serviceTransactions = new TransactionService();
        ILoanServices serviceLoans = new LoanServices();
        IInvoicesService serviceInvoices = new InvoicesSerivce();
        IBrokerService serviceBrokers = new BrokerService();
        IShareService serviceShares = new ShareService();
        public ActionResult Index()
        {
            
                // Default Dictionaries
                Dictionary<string, double> dictAccounts = new Dictionary<string, double>()
            {
                { "accounts", 0}
            };
                ViewBag.PercentageAccounts = dictAccounts;
                Dictionary<string, double> dictTransactions = new Dictionary<string, double>()
            {
                { "transactions", 0}
            };
                ViewBag.PercentageTransactions = dictTransactions;
                Dictionary<string, double> dictInvoices = new Dictionary<string, double>()
            {
                { "invoices", 0}
            };
                ViewBag.PercentageInvoices = dictInvoices;

            Dictionary<string, double> dictShares = new Dictionary<string, double>()
            {
                { "shares", 0}
            };
            ViewBag.PercentageShares = dictShares;

            Dictionary<string, double> dictBrokers = new Dictionary<string, double>()
            {
                { "brokers", 0}
            };
            ViewBag.PercentageBrokers = dictBrokers;

            /***********************************/
            List<Accounts> totalAccounts = serviceAccounts.getAllAccount();
                List<Transaction> totalTransactions = serviceTransactions.GetMany().ToList();
                List<invoice> totalInvoices = serviceInvoices.getAllInvoices();
                List<share> totalShares = serviceShares.GetAllShares();
            List<broker> totalBrokers = serviceBrokers.GetAllBrokers();

            // Totals
            int totalLoans = serviceLoans.GetLoans().Count();
                ViewBag.totalLoans = totalLoans;
                ViewBag.idLastLoan = null;
                if (totalLoans != 0)
                {
                    ViewBag.idLastLoan = totalLoans;
                }
            //ViewBag.totalInvoices = serviceInvoices.GetMany().Count();
            ViewBag.totalBrokers = totalBrokers.Count();
            ViewBag.totalShares = totalShares.Count();

            ViewBag.idLastAccount = null;
                ViewBag.idLastTransaction = null;


                // Review Last Link && ViewBag(dict<T>)
                if (totalAccounts.Count() != 0)
                {
                    ViewBag.idLastAccount = totalAccounts.Last().Id;
                    dictAccounts = getTauxAccounts(totalAccounts);
                    if (dictAccounts.Count() != 0)
                    {
                        ViewBag.PercentageAccounts = dictAccounts;
                    }
                }
                if (totalTransactions.Count() != 0)
                {
                    ViewBag.idLastTransaction = totalTransactions.Last().Id;
                    dictTransactions = getTauxTransactions(totalTransactions);
                    if (dictTransactions.Count() != 0)
                    {
                        ViewBag.PercentageTransactions = dictTransactions;
                    }
                }
                // Shares Fill
                if (totalShares.Count() != 0)
                {
                    dictShares = getTauxShares(totalShares);
                    if (dictShares.Count() != 0)
                    {
                        ViewBag.PercentageShares = dictShares;
                    }
                }

            // Invoices Fill
            if (totalInvoices.Count() != 0)
            {

                dictInvoices = getTauxInvoices(totalInvoices);
                if (dictInvoices.Count() != 0)
                {
                    ViewBag.PercentageInvoices = dictInvoices;
                }
            }

            // Brokers Fill
            if (totalBrokers.Count() != 0)
            {

                dictBrokers = getTauxBrokers(totalBrokers);
                if (dictBrokers.Count() != 0)
                {
                    ViewBag.PercentageBrokers = dictBrokers;
                }
            }


            /***********************************/
            return View();
            

        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "We are ABCD";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "We are ABCD";

            return View();
        }

        // Charts Calculations

        private Dictionary<string, double> getTauxAccounts(IEnumerable<Accounts> accounts)
        {
            int sizeAccounts = accounts.Count();
            IEnumerable<AccountsCurrent> accountsCurrent = accounts.OfType<AccountsCurrent>();
            IEnumerable<AccountsBlocked> accountsBlocked = accounts.OfType<AccountsBlocked>();
            IEnumerable<AccountsSaving> accountsSaving = accounts.OfType<AccountsSaving>();


            double tauxAccountsCurrent = Math.Round((double)(accountsCurrent.Count() * 100 / sizeAccounts));
            double tauxAccountsBlocked = Math.Round((double)(accountsBlocked.Count() * 100 / sizeAccounts));
            double tauxAccountsSaving = Math.Round((double)(accountsSaving.Count() * 100 / sizeAccounts));



            return new Dictionary<string, double>
            {
                { "accounts", sizeAccounts},
                { "current", tauxAccountsCurrent},
                { "blocked", tauxAccountsBlocked},
                { "saving", tauxAccountsSaving}
            };
        }

        private Dictionary<string, double> getTauxInvoices(IEnumerable<invoice> invoices)
        {
            int sizeInvoices = invoices.Count();
            IEnumerable<invoice> invoicesIN = invoices.Where(t => t.InvoiceType.ToUpper() == "IN");
            IEnumerable<invoice> invoicesOUT = invoices.Where(t => t.InvoiceType.ToUpper() == "OUT");


            double tauxInvoicesIN = Math.Round((double)(invoicesIN.Count() * 100 / sizeInvoices));
            double tauxInvoicesOUT = Math.Round((double)(invoicesOUT.Count() * 100 / sizeInvoices));



            return new Dictionary<string, double>
            {
                { "invoices", sizeInvoices},
                { "invoicesIn", tauxInvoicesIN},
                { "invoicesOut", tauxInvoicesOUT}
            };
        }

        private Dictionary<string, double> getTauxTransactions(IEnumerable<Transaction> transactions)
        {
            int sizeTransactions = transactions.Count();
            IEnumerable<Transaction> card = transactions.Where(t => t.Type == TransactionType.Card);
            IEnumerable<Transaction> cheque = transactions.Where(t => t.Type == TransactionType.Cheque);
            IEnumerable<Transaction> payement = transactions.Where(t => t.Type == TransactionType.Payment);
            IEnumerable<Transaction> transer = transactions.Where(t => t.Type == TransactionType.Transfer);
            IEnumerable<Transaction> withdrawal = transactions.Where(t => t.Type == TransactionType.Withdrawal);


            double tauxCard = Math.Round((double)(card.Count() * 100 / sizeTransactions));
            double tauxCheque = Math.Round((double)(cheque.Count() * 100 / sizeTransactions));
            double tauxPayement = Math.Round((double)(payement.Count() * 100 / sizeTransactions));
            double tauxTransfert = Math.Round((double)(transer.Count() * 100 / sizeTransactions));
            double tauxWithdrawal = Math.Round((double)(withdrawal.Count() * 100 / sizeTransactions));



            return new Dictionary<string, double>
            {
                { "transactions", sizeTransactions},
                { "card", tauxCard},
                { "cheque", tauxCheque},
                { "payement", tauxPayement},
                { "transer", tauxTransfert},
                { "withdrawal", tauxWithdrawal}
            };
        }

        private Dictionary<string, double> getTauxShares(IEnumerable<share> shares)
        {
            int sizeShares = shares.Count();
            IEnumerable<share> usd = shares.Where(t => t.currency1.ToUpper().Contains("USD"));
            IEnumerable<share> eur = shares.Where(t => t.currency1.ToUpper().Contains("EUR"));
            IEnumerable<share> tnd = shares.Where(t => t.currency1.ToUpper().Contains("TND"));


            double tauxUSD = Math.Round((double)(usd.Count() * 100 / sizeShares));
            double tauxEUR = Math.Round((double)(eur.Count() * 100 / sizeShares));
            double tauxTND = Math.Round((double)(tnd.Count() * 100 / sizeShares));



            return new Dictionary<string, double>
            {
                { "shares", sizeShares},
                { "usd", tauxUSD},
                { "eur", tauxEUR},
                { "tnd", tauxTND}
            };
        }

        private Dictionary<string, double> getTauxBrokers(IEnumerable<broker> brokers)
        {
            int sizeBrokers = brokers.Count();
            IEnumerable<broker> usd = brokers.Where(t => t.defaultCurrency.ToUpper().Contains("USD"));
            IEnumerable<broker> eur = brokers.Where(t => t.defaultCurrency.ToUpper().Contains("EUR"));
            IEnumerable<broker> tnd = brokers.Where(t => t.defaultCurrency.ToUpper().Contains("TND"));


            double tauxUSD = Math.Round((double)(usd.Count() * 100 / sizeBrokers));
            double tauxEUR = Math.Round((double)(eur.Count() * 100 / sizeBrokers));
            double tauxTND = Math.Round((double)(tnd.Count() * 100 / sizeBrokers));



            return new Dictionary<string, double>
            {
                { "brokers", sizeBrokers},
                { "usd", tauxUSD},
                { "eur", tauxEUR},
                { "tnd", tauxTND}
            };
        }

        public ActionResult getRecentLoans()
        {
            List<loan> listLoans = serviceLoans.GetMany().ToList();
            ;
            List<List<string>> list = new List<List<string>>()
                {
                    new List<string> (){
                        listLoans.Last().IdAccount.ToString(),
                        listLoans.Last().MonthlyPayment.ToString(),
                        listLoans.Last().RestAmount.ToString()
                    },
                    new List<string> (){
                        listLoans.ElementAt(listLoans.Count()-1).IdAccount.ToString(),
                        listLoans.ElementAt(listLoans.Count()-1).MonthlyPayment.ToString(),
                        listLoans.ElementAt(listLoans.Count()-1).RestAmount.ToString()
                    },
                    new List<string> (){
                        listLoans.ElementAt(listLoans.Count()-2).IdAccount.ToString(),
                        listLoans.ElementAt(listLoans.Count()-2).MonthlyPayment.ToString(),
                        listLoans.ElementAt(listLoans.Count()-2).RestAmount.ToString()
                    }
                };
            return Json(list, JsonRequestBehavior.AllowGet);
            //return "hi";
        }


        public ActionResult getRecentAccounts()
        {
            List<Accounts> listAccounts = serviceAccounts.getAllAccount();
            
            if (listAccounts.Count() >= 3)
            {
                List<List<string>> list = new List<List<string>>()
                {
                    new List<string> (){
                        listAccounts.Last().FNameClient.ToString(),
                        listAccounts.Last().Id.ToString(),
                        listAccounts.Last().AccountBalance.ToString()
                    },
                    new List<string> (){
                        listAccounts.ElementAt(listAccounts.Count()-2).FNameClient.ToString(),
                        listAccounts.ElementAt(listAccounts.Count()-2).Id.ToString(),
                        listAccounts.ElementAt(listAccounts.Count()-2).AccountBalance.ToString()
                    },
                    new List<string> (){
                        listAccounts.ElementAt(listAccounts.Count()-3).FNameClient.ToString(),
                        listAccounts.ElementAt(listAccounts.Count()-3).Id.ToString(),
                        listAccounts.ElementAt(listAccounts.Count()-3).AccountBalance.ToString()
                    }
                };
            }

            return Json(new List<List<string>>(), JsonRequestBehavior.AllowGet);
            //return "hi";
        }
    }
}