using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerPortal
{
    public class Tariff
    {
        public Tariff()
        {
            PackageName = "BASIC";
            PricePerMinute = 0.02;
            PricePerSms = 0.02;
            PricePerMegabyte = 8.00;
        }

        public Tariff(string name, double talk, double sms, double priceForOneMegabyte )
        {
            PackageName = name;
            PricePerMinute = talk;
            PricePerSms = sms;
            PricePerMegabyte = priceForOneMegabyte;
        }

        //public Tariff(int type)
        //{
        //    switch (type)
        //    {
        //        case 1:
        //            PackageName = "TALK";
        //            PriceForTalikngMinute = 0.06;
        //            PriceForSmsSending = 0.08;
        //            PriceForOneMegabyte = 10.00;
        //            break;
        //        case 2:
        //            PackageName = "SMS";
        //            PriceForTalikngMinute = 0.08;
        //            PriceForSmsSending = 0.06;
        //            PriceForOneMegabyte = 10.00;
        //            break;
        //        case 3:
        //            PackageName = "WEB";
        //            PriceForTalikngMinute = 0.07;
        //            PriceForSmsSending = 0.07;
        //            PriceForOneMegabyte = 5.00;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public string PackageName { get; set; }
        public double PricePerMinute { get; set; }
        public double PricePerSms { get; set; }
        public double PricePerMegabyte { get; set; }

        public void Display()
        {
            Console.WriteLine("Package = {0}, TalkingMinute = {1}, SMSSend = {2}, 1 Megabyte = {3}", PackageName, PricePerMinute, PricePerSms, PricePerMegabyte);
        }
    }
}
