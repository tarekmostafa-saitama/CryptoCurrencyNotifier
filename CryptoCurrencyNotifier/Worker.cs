using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MarkdownLog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoCurrencyNotifier
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient httpClient;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await APIHandler.GetCurrentState(httpClient);
                Console.WriteLine(result.Select(s => new
                    {
                        CurrencyName = s.Currency,
                        CurrencyValue = s.PriceInUSD,
                    })
                    .ToMarkdownTable());
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
