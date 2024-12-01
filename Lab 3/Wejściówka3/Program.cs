using System.Drawing;

namespace Wejściówka3
{
    public class Ksztalt
    {
        public void PrintIt() => Console.WriteLine("PrintIt Ksztalt ");
    }
    public class Wielokat : Ksztalt
    {
        public void PrintIt() => Console.WriteLine("PrintIt Wielokat");
    }
    public class Kwadrat : Wielokat
    {
        public void PrintIt() => Console.WriteLine("PrintIt Kwadrat");
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Wielokat w = new Kwadrat();
            w.PrintIt();
        }
    }
}