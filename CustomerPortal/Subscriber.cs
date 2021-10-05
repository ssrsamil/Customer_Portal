using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerPortal
{
    class Subscriber
    {
        public Subscriber(string msisdn, decimal balance, Tariff tarif)
        {
            MSISDN = msisdn;
            Balance = balance;
            TarifPackage = tarif;
        }

        public string MSISDN { get; set; } // it has to be used Subscriber creation only
        public decimal Balance { get; set; }
        public Tariff TarifPackage { get; set; }

        public void InvokeMenu()
        {
            Menu menu = new Menu();
            Balance = menu.InitBalance();
            TarifPackage = menu.InitTariff();
        }

        public void AdjustBalance(decimal balance)
        {
            Balance -= balance;
        }

        public void Display()
        {
            if (TarifPackage == null)
            {
                Console.WriteLine("_______________________________________");
                Console.WriteLine("SUBSCRIBER`s DATA");
                Console.WriteLine(" MSISDN       = {0} ", MSISDN);
                Console.WriteLine(" Balance      = {0:0.00} azn ", Balance);
                Console.WriteLine(" Tarif plan has not been selected ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("_______________________________________");
                Console.WriteLine("SUBSCRIBER`s DATA ");
                Console.WriteLine(" MSISDN       = {0} ", MSISDN);
                Console.WriteLine(" Balance      = {0:0.00} azn ", Balance);
                Console.WriteLine(" Tarif plan   = {0} ", TarifPackage.PackageName);
                Console.WriteLine(" Talk/min     = {0:0.00} azn ", TarifPackage.PricePerMinute);
                Console.WriteLine(" SMS          = {0:0.00} azn ", TarifPackage.PricePerSms);
                Console.WriteLine(" 1 MegaByte   = {0:0.00} azn", TarifPackage.PricePerMegabyte);
                Console.WriteLine("_______________________________________");
            }
        }
    }
}
