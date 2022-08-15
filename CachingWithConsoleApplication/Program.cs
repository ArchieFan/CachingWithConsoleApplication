using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CachingWithConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            StockItems PS = new StockItems();
            List<string> Pizzas = (List<string>)PS.GetAvailableStocks();
            Pizzas = (List<string>)PS.GetAvailableStocks();
            Console.WriteLine(String.Join(" ", Pizzas));
            Console.ReadLine();
        }
    }

    public class StockItems
    {
        private const string CacheKey = "availablePizzas";

        public IEnumerable GetAvailableStocks()
        {
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(CacheKey))
                return (IEnumerable)cache.Get(CacheKey);
            else
            {
                IEnumerable availableStocks = this.GetDefaultStocks();

                // Store data in the cache
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(CacheKey, availableStocks, cacheItemPolicy);

                return availableStocks;
            }
        }
        public IEnumerable GetDefaultStocks()
        {
            return new List<string>() { "Pen", "Pencil", "Eraser" };
        }
    }

}
