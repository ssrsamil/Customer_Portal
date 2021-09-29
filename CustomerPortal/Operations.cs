using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerPortal
{
    class Operation
    {
        public static bool checkpoint = false;
        public static DateTime startTime;
        public static decimal bill;
        public static int duration;
        public static double pricePerSecond;

        Subscriber A;
        Thread t;

        public object TarifPackage { get; private set; }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            checkpoint = true;
        }
        public void StartCall(Subscriber A)
        {
            pricePerSecond = A.TarifPackage.PricePerMinute / 60.0;

            if (A.TarifPackage.PackageName == null)
            {
                MessageBox.Show("CAUTION!!! You cannot create a call,the tarif plan has to be choosen.");
            }
            else if (A.Balance <= 0.00M)
            {
                MessageBox.Show("CAUTION!!! You cannot create a call,you have not enough balance.");
            }
            else
            {
                decimal currentBalance = A.Balance;
                duration = 0;

                Console.CancelKeyPress += Console_CancelKeyPress;

                Console.WriteLine();
                Console.WriteLine("To interupt call press Ctrl + C");
                Console.WriteLine();

                startTime = DateTime.Now;
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

                while (!checkpoint && currentBalance > (Decimal)(duration * pricePerSecond))
                {
                    Thread.Sleep(1000);
                    duration++;
                }

                bill = (Decimal)(duration * pricePerSecond);
                A.AdjustBalance(bill);
                MessageBox.Show("Son istifade " + bill);
                checkpoint = false;
                Thread.CurrentThread.Abort();
            }
        }

        public string ShowBalance(Subscriber sub)
        {
            return sub.Balance.ToString();
        }

        public TimeSpan Duration(DateTime startTime)
        {
            TimeSpan diff1 = DateTime.Now - startTime;
            return diff1;
        }
        //public string CallDurationDisplay(string seconds, double result, Subscriber A)
        //{
        //    return "Duration : " + seconds + ", Son istifade = " + result + " azn, Cari balans = " + A.Balance;
        //}

        public string CallDurationDisplay(double result, Subscriber A)
        {
            return "Son istifade = " + result + " azn, Cari balans = " + A.Balance;
        }
        public string CallDurationDisplay(string result, Subscriber A)
        {            
            return "Son istifade = " + result + " azn, Cari balans = " + A.Balance;
        }
        //public string CallDurationDisplay(Subscriber A)
        //{
        //    return "Cari balans = " + A.Balance;
        //}
    }
}
