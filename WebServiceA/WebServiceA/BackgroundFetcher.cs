using RestSharp;
using System.Runtime.Caching;
using System.Text.Json;

namespace WebServiceA
{
    public class BackgroundFetcher : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundFetcher> logger;
        private Timer timer;
        private int number;

        public BackgroundFetcher(ILogger<BackgroundFetcher> logger) { 
            this.logger = logger;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var client = new RestClient();
            var request = new RestRequest("https://bitcoinpricecalc.com/api?action=price&currency=USD", Method.Get);

            timer = new Timer(o => {
                ObjectCache cache = MemoryCache.Default;
                Interlocked.Increment(ref number);

                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(10),

                };
                RestResponse response = client.Execute(request);
                var price = Double.Parse(JsonDocument.Parse(response.Content).RootElement.GetProperty("price").ToString());

                Console.WriteLine("price fetched = " + price.ToString());

                cache.Add(new CacheItem("price" + number.ToString(), price), cacheItemPolicy);


            },
            null,
            TimeSpan.Zero,
            TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

