using Boyner.Service;
using System;
using Core.MessageBroker.Concrete;

namespace Boyner.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ////db'ye bağlanmaması durumunda cache'den en son okuduğu değeri getirir.
            //for (int i = 0; i < 10; i++)
            //{

            string connectionString =
                "Server=localhost;Database=BoynerCase;Trusted_Connection=True;MultipleActiveResultSets=true";
            int timeMins = 1;
            ConfigurationReader service = new ConfigurationReader("SERVICE-A", connectionString, timeMins);
            string result = service.GetValue<string>("SERVICE-A");
            Console.WriteLine(result);
            //}

            //MessageBroker ms = new MessageBroker();
            //ms.Publisher("myqueue", "new config added");
        }
    }
}
