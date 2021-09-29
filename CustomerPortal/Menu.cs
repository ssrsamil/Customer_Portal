using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerPortal
{
    class Menu
    {
        public Decimal InitBalance()
        {
            Console.WriteLine();
            Console.WriteLine("Enter initial balance for subscriber");
            decimal balance = Convert.ToDecimal(Console.ReadLine());
            return balance;
        }
        public Tariff InitTariff()
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Please select tariff package");
            Console.WriteLine("         * Enter 1 if you talking a lot ");
            Console.WriteLine("         * Enter 2 if you send sms a lot ");
            Console.WriteLine("         * Enter 3 if you use internet ");
            Console.WriteLine("         * Enter 4 create custom tarif ");
            Console.WriteLine("         * Enter 5 to skip this step ");
            Console.WriteLine("-------------------------------------------------------------");

            int tarifA = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Tariff tA;

            if (tarifA == 4)
            {
                Console.WriteLine(" * Enter price for 1 minute talking ");
                double minutePriceA = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine(" * Enter price for sms ");
                double smsPriceA = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine(" * Enter price for 1 MB ");
                double megaPriceA = Convert.ToDouble(Console.ReadLine());

                tA = new Tariff("Custom Tarif", minutePriceA, smsPriceA, megaPriceA);
            }

            else if (tarifA == 5)
            {
                tA = new Tariff();
            }
            else
                tA = new Tariff();
            return tA;
        }
    }
}
