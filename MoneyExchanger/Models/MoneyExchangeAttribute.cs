using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyExchanger.Models
{
    public class MoneyExchangeAttribute
    {
        public string VoucherID { get; set; }
        public DateTime CurrentDate{ get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }
        public IEnumerable<SelectListItem> CurrencyList{ get; set; }
    }
    public class CurrencyCodes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class ConversionCodes
    {
        public string LocationCode { get; set; }
        public string CurrencyCode { get; set; }
        public string Varience { get; set; }
        public string Stock { get; set; }
        public string BuyRate{ get; set; }
        public string SellRate { get; set; }
        public string Units { get; set; }
        public string AvgCost { get; set; }
        
    }
}