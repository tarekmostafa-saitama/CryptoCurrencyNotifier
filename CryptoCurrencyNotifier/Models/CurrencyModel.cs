using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCurrencyNotifier.Models
{
    public class CurrencyModel
    {
        public string Currency { get; set; }
        public decimal PriceInUSD { get; set; }
    }
}
