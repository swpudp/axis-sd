using AxisOrder.ServcieContracts;
using AxisOrder.SoapMiddleware;
using System;
using System.ServiceModel;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadKey();
        }


        static void Test()
        {
            var uri = "http://localhost:22451/ProductService.svc";
            using (var client = new EsbClient())
            {
                var service = client.GetClientProxy<IProductService>(uri);
                for (var i = 0; i < 1000; i++)
                {
                    var name = service.GetByCode($"{Guid.NewGuid()}");
                    Console.WriteLine(name);
                }
            }
        }
    }
}
