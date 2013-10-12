using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyExchanger.Models;
using System.Data;

namespace MoneyExchanger.Controllers
{
    public class MoneyMasterController : Controller
    {
        //
        // GET: /MoneyMaster/

        public ActionResult GetMasterData()
        {
            ViewBag.Title = "Money Exchanger";
            MoneyExchangeAttribute objMonExchng = new MoneyExchangeAttribute();
            Random rnd = new Random(0);
            objMonExchng.VoucherID = rnd.Next().ToString();
            objMonExchng.CurrentDate = DateTime.Now;

            List<SelectListItem> custList = new List<SelectListItem>{new SelectListItem{ Value = "1", Text = "Ganesh Textiles" },
                    new SelectListItem { Value = "2", Text = "Ganesh Textiles" },
                    new SelectListItem { Value = "3", Text = "Ganesh Textiles" },
                    new SelectListItem { Value = "4", Text = "Ganesh Textiles" }
                };


            List<SelectListItem> currencyList = new List<SelectListItem>{new SelectListItem{ Value = "sgd", Text = "Singapore Dollars" },
                    new SelectListItem { Value = "MYR", Text = "Malaysian Ringetts" },
                    new SelectListItem { Value = "USD", Text = "US Dollars" },
                    new SelectListItem { Value = "INR", Text = "Indian Rupees" }
                };
            objMonExchng.CustomerList = custList;
            objMonExchng.CurrencyList = currencyList;
            return View(objMonExchng);
        }

        public ActionResult GetExchangeDate(string hi)
        {

            LinqMasterDataContext a = new LinqMasterDataContext();
            var varCurr = (from curr in a.TblCurrencies
                       select new CurrencyCodes
                       {
                           Code = curr.CurrencyCode,
                           Description = curr.CurrencyName,
                           Varience = Convert.ToDecimal(curr.Varience)
                       }).ToList<CurrencyCodes>();
            var varUsers = (from usrs in a.TblUserMasters
                            select new UserMaster
                            {
                                UserId = usrs.UserId,
                                Description = usrs.Description,
                                Password = usrs.Password
                            }).ToList<UserMaster>(); 
            return Json(new { varCurr, varUsers }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetExactMasterData()
        {
            //if (Check.getValue() != "00E0B406E1ECBFEBFBFF000106CAWKXY Technology inc.Base BoardTo be filled by O.E.M.")
            //{
            //    return View("ErrorView");
            //}

            return View();
        }
        public ActionResult GetExchangeRates(string currencyCode)
        {
            LinqMasterDataContext mastContext = new LinqMasterDataContext();
            var convertionList = mastContext.GetLocationCurrency("L1", currencyCode).First();


            return Json(convertionList, JsonRequestBehavior.AllowGet); ;
        }
        public ActionResult SaveTransactions(string currCode, string userId, string transactionType, string Rate, string ForeignAmount, string LocalAmount, string AvgCost, string AvgStock, int print)
        {
            LinqMasterDataContext mastContext = new LinqMasterDataContext();
            System.Data.Linq.ISingleResult<SaveTransactionResult> ret = mastContext.SaveTransaction(userId, currCode, transactionType, Convert.ToDecimal(Rate), Convert.ToDecimal(ForeignAmount), Convert.ToDecimal(LocalAmount), Convert.ToDecimal(AvgCost), Convert.ToDouble(AvgStock));
            decimal TranNo = 0;
            TranNo = ret.First<SaveTransactionResult>().TranNo;
            //if (ret != null && ret.Any())
            //{
            //    foreach (var item in ret)
            //    {
            //        TranNo = item.TranNo;
            //    }
                
            //}
            var compDetails = mastContext.tblCompanies.FirstOrDefault();
            var AdditionaValues = new { currCode = currCode, transactionType = transactionType, ForeignAmount = ForeignAmount, LocalAmount = LocalAmount, Rate = Rate };
            if (print == 1) {
                PrintReceipt(TranNo, currCode, userId, transactionType, Rate, ForeignAmount, LocalAmount, AvgCost);
            }

            return Json(new {compDetails, AdditionaValues }, JsonRequestBehavior.AllowGet);
        }
        public void PrintReceipt(decimal TranNo, string currCode, string userId, string transactionType, string Rate, string ForeignAmount, string LocalAmount, string AvgCost)
        {
            LinqMasterDataContext mastContext = new LinqMasterDataContext();
            PrintTransaction rcpt = new PrintTransaction();
            var compDetails = mastContext.tblCompanies.FirstOrDefault();
            //var AdditionaValues = new { currCode = currCode, transactionType = transactionType, ForeignAmount = ForeignAmount, LocalAmount = LocalAmount, Rate = Rate };
            rcpt.Description = compDetails.Description;
            rcpt.Address1 = compDetails.Address1;
            rcpt.Address2 = compDetails.Address2;
            rcpt.Address3 = compDetails.Address3;
            rcpt.Phone = compDetails.Phone;
            rcpt.TranNo = TranNo.ToString();
            rcpt.CurCode = currCode;
            rcpt.TranType = transactionType;
            rcpt.TaxRef = compDetails.TaxRef;
            rcpt.ForeignAmt = ForeignAmount;
            rcpt.LocalAmt = LocalAmount;
            rcpt.Rate = Rate;
            rcpt.print();
            

        }
    }
}
