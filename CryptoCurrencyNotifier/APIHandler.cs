using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrencyNotifier.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoCurrencyNotifier
{
    public class APIHandler
    {
        public static async Task<List<CurrencyModel>> GetCurrentState(HttpClient httpClient)
        {
            var list = new List<CurrencyModel>();
            var result = await httpClient.GetAsync(
                "https://api.nomics.com/v1/currencies/ticker?key=cf69acb97f9ab055b1eadc6b433a9f56&ids=BTC,ETH,XRP&interval=1d,30d&convert=USD");
            var resultAsString = await result.Content.ReadAsStringAsync();
            dynamic resultAsJson = JsonConvert.DeserializeObject(resultAsString);
            foreach (var temp in resultAsJson)
            {
               list.Add(new CurrencyModel(){Currency = temp.name , PriceInUSD = temp.price});
            }
            return list;
        }
    }

    
}
