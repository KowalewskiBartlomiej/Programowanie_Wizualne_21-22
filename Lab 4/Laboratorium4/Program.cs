using System;
using System.Collections;
using System.Linq;

namespace Laboratorium4
{
    //ZADANIE 1,2,3,4,5
    public class TestDelegate
    {
        public delegate string PrintIt(string s);
        public delegate void AnonimDelegate(int i);
        public int liczba = 1;


        public string PrintString(string s)
        {
            return "object: " + s;
        }

        public void zwiekszliczbe(int i)
        {
            this.liczba =+ i;
        }

    }

    //ZADANIE 13,14,15
    public class Worker
    {
        public int ID;
        public string Name;
    }

    //ZADANIE 16

    public static class Extensions
    {
        public static int SumOfDigits(this int i)
        {
            int sum = 0;
            char[] tab = i.ToString().ToCharArray();
            for (int d = 0; d < tab.Length; d++)
            {
                sum += int.Parse("" + tab[d]);
            }
            return sum;
        }

        public static int Reversed(this int i)
        {
            string s = "";
            char[] tab = i.ToString().ToCharArray();
            for (int d = tab.Length-1; d >= 0; d--)
            {
                s += tab[d];
            }
            return int.Parse(s);
        }


    }


    internal class Program
    {
        //Zadanie 1,2,3,4,5
        public static string StaticPrintString(string s)
        {
            return "static: " + s;
        }

        public static void ZwiekszLiczbe (int liczba, TestDelegate testDelegate)
        {
            TestDelegate.AnonimDelegate zwieksz = delegate (int a)
            {
                testDelegate.liczba += a;
                Console.WriteLine(testDelegate.liczba);
            };
        }

        //Zadanie 8
        public static bool IsOdd(int i)
        {
            return (i % 2 == 1);
        }

        static void Main(string[] args)
        {
            //ZADANIE 2,3
            //Delegat dla metody obiektu
            TestDelegate testDelegate = new TestDelegate();
            TestDelegate.PrintIt printIt = new TestDelegate.PrintIt(testDelegate.PrintString);
            //Delegat dla metody statycznej
            TestDelegate.PrintIt staticPrintIt = new TestDelegate.PrintIt(Program.StaticPrintString);
            //Delegat dla kilku metod
            TestDelegate.PrintIt printIt2 = new TestDelegate.PrintIt(Program.StaticPrintString);
            printIt2 += testDelegate.PrintString;

            Console.WriteLine(printIt("haslo"));
            Console.WriteLine(staticPrintIt("haslo"));
            Console.WriteLine(printIt2("haslo"));

            ZwiekszLiczbe(2, testDelegate);

            //Zadanie 7
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

            foreach (int i in arr.Where(i => i%2==1))
            {
                Console.WriteLine(i);
            }

            //Zadanie 8

            Func<int, bool> parityChecker = new Func<int, bool>(IsOdd);
            foreach (int i in arr.Where(parityChecker))
                Console.WriteLine(i);

            //ZADANIE 11, 12

            //ARRAYLIST
            DateTime dateTime = DateTime.Now;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < 10e6; i++)
            {
                arrayList.Add(i);
            }
            TimeSpan timeSpan = DateTime.Now - dateTime;
            Console.WriteLine(timeSpan);



            DateTime dateTime1 = DateTime.Now;
            foreach (int i in arrayList)
            {
                int a = i;
            }
            TimeSpan timeSpan1= DateTime.Now - dateTime1;
            Console.WriteLine(timeSpan1);

            //LIST
            DateTime dateTime2 = DateTime.Now;
            List<int> List = new List<int>();
            for (int i = 0; i < 10e6; i++)
            {
                List.Add(i);
            }
            TimeSpan timeSpan2 = DateTime.Now - dateTime2;
            Console.WriteLine(timeSpan2);



            DateTime dateTime3 = DateTime.Now;
            foreach (int i in List)
            {
                int a = i;
            }
            TimeSpan timeSpan3 = DateTime.Now - dateTime3;
            Console.WriteLine(timeSpan3);

            //ZADANIE 13,14,15

            SortedDictionary<int, Worker> sortedDict = new SortedDictionary<int, Worker>()
            {
                    {1, new Worker(){ ID=1, Name="Grzesiek"}},
                    {2, new Worker(){ ID=2, Name="Jurek"}},
                    {3, new Worker(){ ID=3, Name="Marek"}}
            };

            foreach (var p in sortedDict)
            {
                Console.WriteLine("Key: {0} Value: {1} {2}", p.Key, p.Value, p.Value.GetHashCode());
            }
            KeyValuePair<int, Worker> kvp = new KeyValuePair<int, Worker>(2, new Worker() { ID = 2, Name = "Marekk" });
            foreach (var p in sortedDict.Values.Where(p => p.Name == kvp.Value.Name))
            {
                Console.WriteLine("zawiera");
            }

            //ZADANIE 16

            int number = 12345678;
            Console.WriteLine(number.SumOfDigits());
            Console.WriteLine(number.Reversed());


        }
    }
}