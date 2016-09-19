using ERP_BANK.Domain.Entities;
using ERP_BANK.Service;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_BANK.Web.Controllers
{
    [Authorize]
    public class AccountBankController : Controller
    {
        IAccountsService acse = new AccountsService();

        //public AccountBankController()
        //{
        //    IAccountsService acse = new AccountsService();
        //}
        //public AccountBankController(IAccountsService acse)
        //{
        //    this.acse = acse;
        //}
        // GET: AccountBank ActualizeAccount();
        //public ActionResult Index()
        //{

        //    //ActualizeAccount();
        //    IEnumerable<Accounts> accounts = acse.GetMany().ToList();
        //    return View(accounts);
        //}

            public ActionResult Index()
        {
            IEnumerable<Accounts> accounts = acse.GetMany().ToList();
            //AccountsSaving a = (AccountsSaving)accounts.Last();
            
            List<IDictionary< string, string>> list = new List<IDictionary<string, string>>();
            if (accounts.Count() != 0)
            {
                foreach (var item in accounts)
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>()
                        {
                            { "Id" , null},
                            { "Type" , null},
                            { "First Name" , null},
                            { "Last Name" , null},
                            { "Cin" , null},
                            { "Account Balance" , null},
                            { "Libelle" , null},
                            { "Currency" , null},
                            { "Note" , null},
                            { "Salary" , null},
                            { "Blocked", null},
                            { "Date Deblocked" , null}
                        };

                    dict["Id"] = item.Id.ToString();
                    dict["Type"] = item.Type.ToString();
                    dict["First Name"] = item.FNameClient.ToString();
                    dict["Last Name"] = item.LNameClient.ToString();
                    dict["Cin"] = item.CinClient.ToString();
                    dict["Account Balance"] = item.AccountBalance.ToString();                    
                    dict["Libelle"] = item.Libelle.ToString();
                    dict["Currency"] = item.Currency.ToString();
                    dict["Note"] = item.Note.ToString();
                    dict["Salary"] = item.salary.ToString();
                    if (dict["Type"]== "Saving Account")
                    {

                        AccountsSaving x = (AccountsSaving)item;
                        dict["Blocked"] = x.Blocked.ToString();
                        dict["Date Deblocked"] = x.DateofSaving.ToString();

                    }
                    else if (dict["Type"] == "Blocked Account")
                    {
                        //AccountsSaving account = (AccountsSaving)acse.GetById(dict["Id"]);
                        //AccountsBlocked x = (AccountsBlocked)item;
                        // dict["Blocked"] = account.Blocked.ToString();
                        //dict["Date Deblocked"] = account.DateofSaving.ToString();
                        AccountsBlocked x = (AccountsBlocked)item;
                        dict["Blocked"] = x.Blocked.ToString();
                        dict["Date Deblocked"] = x.DateDeblocked.ToString();
                    }
                    else
                    {
                        dict["Blocked"] = "____";
                        dict["Date Deblocked"] = "____";
                    }
                    
                    list.Add(dict);
                }
            }
            
            ViewBag.accounts = list;
            
            return View();
        }

        /*public ActionResult ListAccountBlocked()
        {
            IEnumerable<AccountsBlocked> accounts = acse.GetMany().OfType<AccountsBlocked>().ToList();
            return View();
        }*/
        public ActionResult IndexBlocked(int id)
        {
            return View();
        }

        // GET: AccountBank/Details/5
        public ActionResult Details(int id)
        {
            Accounts account = acse.GetById(id);
            return View(account);
           
        }

        // GET: AccountBank/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountBank/Create
        [HttpPost]
        public ActionResult Create(AccountsCurrent a) 
        {
            if (ModelState.IsValid)
            {
                acse.AddAccount(a);
                acse.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult CreateBlocked()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateBlocked(AccountsBlocked a)
        {
            if (ModelState.IsValid)
            {
                acse.addAccountBlocked(a);
                acse.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult CreateSaving()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateSaving(AccountsSaving a)
        {
            if (ModelState.IsValid)
            {
                acse.addAccountSaving(a);
                acse.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: AccountBank/Edit/5
        public ActionResult Edit(int id)
        {
            Accounts a=acse.GetById(id);
            return View(a);
        }

        // POST: AccountBank/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Accounts a = acse.GetById(id);

            a.FNameClient = collection["FNameClient"];
            a.LNameClient= collection["LNameClient"];
            a.CinClient = collection["CinClient"];
            a.AccountBalance = float.Parse(collection["AccountBalance"]);
            a.Currency = collection["Currency"];
            a.Libelle = collection["Libelle"];
            a.salary = float.Parse(collection["salary"]);
            a.Note= collection["Note"];            
            acse.Update(a);
            acse.Commit();

            if (ModelState.IsValid)
            {
               
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

      /*  public ActionResult Edit(int id, AccountsBlocked a)
        {
            if (ModelState.IsValid)
            {
                acse.Update(a);
                acse.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id, AccountsSaving a)
        {
            if (ModelState.IsValid)
            {
                acse.Update(a);
                acse.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }*/

        // GET: AccountBank/Delete/5
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                Accounts account = acse.GetById(id);
                acse.Delete(account);
                acse.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // POST: AccountBank/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Accounts a)
        {
            return View();
        }


        private String randomchar()
        {
            String alphabet = "0123456789ABCDE";
            int N = alphabet.Length;

            Random r = new Random();
            String text = "";
            for (int i = 0; i < 10; i++)
            {
                text += (alphabet.ElementAt(r.Next(N)));
            }
            return text;
        }
        public ActionResult PDF(int id)
        {
            Accounts evt = acse.GetById(id);
            var x = randomchar();

            Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Desktop/" + x + ".pdf", FileMode.Create));

            doc.Open();
            Paragraph p = new Paragraph("Event Details");

            p.Alignment = Element.ALIGN_CENTER;
            doc.Add(p);

            //Image img = Image.GetInstance("/Content/Picture/bank-gray-md.png");
            //img.Alignment = Element.ALIGN_RIGHT;
            //doc.Add(img);


            p = new Paragraph("Cin Client: " + evt.CinClient);


            // p.Alignment = Element.ALIGN_JUSTIFIED;

            doc.Add(p);
            p = new Paragraph("First Name Client  :" + evt.FNameClient);
            
            //  p.Alignment = Element.ALIGN_LEFT;
            doc.Add(p);
            

            p = new Paragraph("Last NAme Client " + evt.LNameClient);
            //  p.Alignment = Element.ANCHOR;
            doc.Add(p);

            doc.Close();

            System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Desktop/" + x + ".pdf");
            //return (Content("<script language='javascript' type='text/javascript'>alert('Download Successfull');</script>"));
            
            return RedirectToAction("Index");
        }

        public void ActualizeAccount()
        {
            acse.BanefitofAccountBlocked();
            acse.BenefitOfAccountSaving();
            acse.DeblockedAcchountBlocked();
            acse.DeblockedAcchountSaving();
        }
    }
}
