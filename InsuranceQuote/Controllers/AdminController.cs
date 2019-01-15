using InsuranceQuote.ViewModels;
using InsuranceQuote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceQuote.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (InsuranceQuotesEntities db = new InsuranceQuotesEntities())
            {
                List<Quote> quotes = db.Quotes.Where(x => x.Quote1 != null).ToList();
                var quoteVms = new List<QuoteVm>();
                foreach (var quote in quotes)
                {
                    var quoteVm = new QuoteVm();
                    quoteVm.FirstName = quote.FirstName;
                    quoteVm.LastName = quote.LastName;
                    quoteVm.EmailAddress = quote.EmailAddress;
                    quoteVm.Quote = Convert.ToString(quote.Quote1);

                    quoteVms.Add(quoteVm);

                }
                return View(quoteVms);

            }
        }
    }
}