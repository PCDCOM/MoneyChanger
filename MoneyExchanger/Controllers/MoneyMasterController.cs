﻿using System;
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
            Random  rnd = new Random(0);
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

        public ActionResult GetExchangeDate(string hi){
         
            LinqMasterDataContext a = new LinqMasterDataContext();
            var abc = (from curr in a.TblCurrencies
                      select new CurrencyCodes
                      {
                         Code =  curr.CurrencyCode,
                          Description = curr.CurrencyName
                      }).ToList<CurrencyCodes>();
           
            return Json(abc, JsonRequestBehavior.AllowGet);
           
        }
        public ActionResult GetBoardDisplayData(string hi)
        {

            LinqMasterDataContext a = new LinqMasterDataContext();
            var abc = (from curr in a.TblCurrencies
                       select new CurrencyCodes
                       {
                           Code = curr.CurrencyCode,
                           Description = curr.CurrencyName
                       }).ToList<CurrencyCodes>();

            return Json(abc, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetBoardDisplay(string hi)
        {

            return View();

        }
        public ActionResult GetExactMasterData()
        {
            

            return View();
        }
        public ActionResult GetExchangeRates(string currencyCode)
        {
            LinqMasterDataContext mastContext = new LinqMasterDataContext();
            var convertionList = mastContext.GetLocationCurrency("L1", currencyCode).First();
            return Json(convertionList, JsonRequestBehavior.AllowGet); ;
        }
        public ActionResult SaveTransactions(string currCode, string transactionType, string Rate,string ForeignAmount, string LocalAmount, string AvgCost, string AvgStock)
        {
            LinqMasterDataContext mastContext = new LinqMasterDataContext();
            var resultSet = mastContext.SaveTransaction("FE", currCode, transactionType, Convert.ToDecimal(Rate), Convert.ToDecimal(ForeignAmount), Convert.ToDecimal(LocalAmount), Convert.ToDecimal(AvgCost), Convert.ToDouble(AvgStock));
            return Json(resultSet, JsonRequestBehavior.AllowGet); ;
        }

    }
}
