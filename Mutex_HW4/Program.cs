using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutex_HW4
{
    internal class Program // task 4 
    {
        public static Mutex mtx = new Mutex();

        static void Main(string[] args)
        {

            Thread[] threads = { new Thread(WriteNumbers), 
                new Thread(WrirePrimeNumbers), 
                new Thread(WritePrimeNumbers_2) };
            foreach (var thread in threads)
            thread.Start();// thread.Join(); 
           
 
        }


        //METHODS 
        static void WriteNumbers()
        {
            mtx.WaitOne();
            Random random = new Random();
            StreamWriter wr = new StreamWriter("numbers.txt");
            for (int i = 0; i < 100; i++) wr.WriteLine(random.Next(1,500));
            wr.Close();
            mtx.ReleaseMutex();

            Console.WriteLine("First thread off");
        }

        static void WrirePrimeNumbers()
        {
            mtx.WaitOne();
            List<int> numbers = new List<int>();   
            
            //read
            StreamReader rd = new StreamReader("numbers.txt");
            while (!rd.EndOfStream) {
                string s = rd.ReadLine();   
                if(IsPrime(Int32.Parse(s)))numbers.Add(Int32.Parse(s));
            }
            rd.Close();

            //write
            StreamWriter wr = new StreamWriter("PrimeNumbers.txt");
            foreach(int numb in numbers) wr.WriteLine(numb);
            wr.Close();
            mtx.ReleaseMutex();
            Console.WriteLine("Second thread off");

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
            foreach (int numb in numbers)if(numb%10 == 7)wr.WriteLine(numb);
            wr.Close();
            mtx.ReleaseMutex();

            Console.WriteLine("Third thread off");
        }

        public static bool IsPrime(int i)
        {
            for (int p = 2; p <= Math.Sqrt(i); ++p) if (i % p == 0) return false;
            return true;
        }
    }
}
