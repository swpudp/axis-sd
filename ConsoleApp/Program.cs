using AxisOrder.ServcieContracts;
using AxisOrder.SoapMiddleware;
using System;
using System.Net;
using System.ServiceModel;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test();
            //GetSso();
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

        static void GetSso()
        {
            var url = "http://test.passport.changhong.com/fetchKey.do?appId=bills-test";
            var client = new WebClient();
            var bytes = client.DownloadData(url);
            var response = Encoding.UTF8.GetString(bytes);
            Console.Write(response);
        }
    }
}
