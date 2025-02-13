using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Mutex_HW4
{
    internal class Program 
    {
        static Mutex mtx = new Mutex();
        private static int MainBet = 0;
       

        static void Main(string[] args)
        {

#if true4 // task4  
            Thread[] threads = { new Thread(WriteNumbers),
                new Thread(WrirePrimeNumbers),
                new Thread(WritePrimeNumbers_2) };


            foreach (var thread in threads)
            {thread.Start();  thread.Join(); }
#endif
            // casino (task 6). the third one is in another proj)
            int all_gambles = new Random().Next(20, 21);
            Console.WriteLine($"We have {all_gambles} players");
            Thread UpdateBet = new Thread(UpdateMainBet); UpdateBet.Start(); 
            

            Semaphore semaphore = new Semaphore(5, 5);
             for (int i = 0; i < all_gambles; ++i)
                ThreadPool.QueueUserWorkItem(GambleTable, semaphore);


        }
        // MAINMETHOD END 


        static Random rnd = new Random();
        static void GambleTable(object a)
        {
            Semaphore s = a as Semaphore;
            s.WaitOne();
            int Money = rnd.Next(50, 500); // here could be player class but I made just variable.
            bool Staying = true;

            while(Money > 50 & Staying) // he keeps playing till deciding to leave or losing too much money.
            {
                int Bet = rnd.Next(50, Money);
                int number = rnd.Next(1, 15);
                if (number == MainBet) Money += Bet;
                else Money -= Bet;
                Staying = rnd.Next(2) == 1;
                Console.WriteLine($"Player put {Bet}$ for {number}. Current status: {Money}$");
                if (!Staying)Console.WriteLine($"Player left");
   
                Thread.Sleep(3000); // just give time the main bet to change
            }
           
            s.Release();
        }

        static void UpdateMainBet()
        {
            while (true)
            {
                MainBet = rnd.Next(1, 15);
                Console.WriteLine($"MAIN BET: {MainBet}");
                Thread.Sleep(2500);
            }
        }
        #region  For mutex task

        static void WriteNumbers()
        {
            mtx.WaitOne();
            Random random = new Random();
            StreamWriter wr = new StreamWriter("numbers.txt");
            for (int i = 0; i < 100; i++) wr.WriteLine(random.Next(1, 500));
            wr.Close();

            mtx.ReleaseMutex();

        }

        static void WrirePrimeNumbers()
        {
            mtx.WaitOne();
            List<int> numbers = new List<int>();

            //read
            StreamReader rd = new StreamReader("numbers.txt");
            while (!rd.EndOfStream)
            {
                string s = rd.ReadLine();
                if (IsPrime(Int32.Parse(s))) numbers.Add(Int32.Parse(s));
            }
            rd.Close();

            //write
            StreamWriter wr = new StreamWriter("PrimeNumbers.txt");
            foreach (int numb in numbers) wr.WriteLine(numb);
            wr.Close();

            mtx.ReleaseMutex();

        }

        static void WritePrimeNumbers_2()
        {
            mtx.WaitOne();
            List<int> numbers = new List<int>();
            StreamReader rd = new StreamReader("PrimeNumbers.txt");
            while (!rd.EndOfStream)
            {
                string s = rd.ReadLine();
                if (IsPrime(Int32.Parse(s))) numbers.Add(Int32.Parse(s));
            }
            rd.Close();
            StreamWriter wr = new StreamWriter("PrimeNumbersRestSeven.txt");
            foreach (int numb in numbers) if (numb % 10 == 7) wr.WriteLine(numb);
            wr.Close();

            mtx.ReleaseMutex();

        }

        public static bool IsPrime(int i)
        {
            for (int p = 2; p <= Math.Sqrt(i); ++p) if (i % p == 0) return false;
            return true;
        } 
        #endregion
    }
}
