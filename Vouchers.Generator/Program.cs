using System;
using System.IO;
using System.Linq;

namespace Vouchers.Generator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var vouchers = File.ReadAllLines("Vouchers.csv")
                .Select(s => new
                {
                    Name = s.Split('|')[0],
                    Price = s.Split('|')[1],
                    ProductCodes = s.Split('|')[2]
                }).ToArray();

            var file = File.Open("..\\..\\..\\Vouchers\\data.json", FileMode.Create, FileAccess.ReadWrite);
            var writer = new StreamWriter(file);
            writer.WriteLine("[");
            var random = new Random();
            for (var i = 0; i < 100000; i++)
            {
                var voucher = vouchers[random.Next(vouchers.Length - 1)];
                writer.WriteLine($"    {{Id:\"{Guid.NewGuid()}\",Name:\"{voucher.Name}\",Price:{voucher.Price},ProductCodes:\"{voucher.ProductCodes}\"}},");
            }
            writer.WriteLine("]");
            writer.Flush();
            file.Close();
        }
    }
}
