using InsuranceQuote.Models;
using InsuranceQuote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceQuote.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Success(string lastQuote)
        { return View(); }
        public string lastQuote = "lastQuote";
        [HttpPost]
        public ActionResult GetQuote(string firstName, string lastName, string emailAddress, 
                                        string dOB, string carYear, string carMake, string carModel,
                                        string tickets, string dUI, string insType)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || 
                string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(dOB) ||
                string.IsNullOrEmpty(carYear) || string.IsNullOrEmpty(carMake) ||
                string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(tickets) ||
                string.IsNullOrEmpty(dUI) || string.IsNullOrEmpty(insType))
            
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (InsuranceQuotesEntities db = new InsuranceQuotesEntities())
                {
                    var quote = new Quote();
                    quote.FirstName = firstName;
                    quote.LastName = lastName;
                    quote.EmailAddress = emailAddress;
                    quote.DOB = Convert.ToDateTime(dOB);
                    quote.CarYear = Convert.ToInt32(carYear);
                    quote.CarMake = carMake;
                    quote.CarModel = carModel;
                    quote.Tickets = Convert.ToInt32(tickets);
                    quote.DUI = dUI;
                    quote.InsType = insType;

                    var today = DateTime.Now;
                    var age = today.Year - quote.DOB.Year;
                    decimal x = 50 + (10 * quote.Tickets);  
                    
                        if (age < 18)
                        {
                        x = x + 100; 
                        }
                        else if (age < 18)
                        {
                        x = x + 25;
                         }
                        else if (age > 100)
                    { x = x + 100; }
                        if (quote.CarYear < 2000)
                    { x = x + 25;  }
                        if (quote.CarYear > 2015)
                    { x = x + 25;  }
                        if (quote.CarMake == "Porsche")
                    { x = x + 25; }
                        if (quote.CarMake == "Porsche" && quote.CarModel == "911 Carrera")
                    { x = x + 25;  }
                        if (quote.DUI == "yes")
                    { x = x + (x / 4); }
                        if (quote.InsType == "full")
                    { x = x + (x / 2); }




                    quote.Quote1 = x;

                    db.Quotes.Add(quote);
                    db.SaveChanges();

                    int userid = quote.Id;
                return RedirectToAction("Success", new { Id = userid });
                }

                //var quoteVMs = new List<QuoteVm>();
                //foreach (var quote in quoteVMs)
                //{
                //    var quoteVM = new QuoteVm();
                //    quoteVM.ID = quote.ID;
                //    quoteVM.FirstName = quote.FirstName;
                //    quoteVM.LastName = quote.LastName;
                //    quoteVM.Quote = quote.Quote;
                //    quoteVMs.Add(quoteVM);

                //    lastQuote = Convert.ToString(quote.Quote);
                    
                //}
               

            

            }
        }

    
    }
}