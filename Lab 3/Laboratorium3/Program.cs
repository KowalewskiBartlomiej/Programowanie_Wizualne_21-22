using System.Collections;
using System.Globalization;

namespace Laboratorium3
{
    public interface IPrintable
    {
        void Print(string text)
        {
            Console.WriteLine(this.ID);
        }
        Guid ID { get; set; }
    }

    public interface IPublicPrintable
    {
        void Print(string text)
        {
            Console.WriteLine(text);
        }
    }

    abstract public class Worker:IPrintable, IPublicPrintable, IComparable
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public System.Guid id = System.Guid.NewGuid();
        public int data_urodzenia { get; set; }
        Guid IPrintable.ID { get; set; }

        public Worker() { }

        public Worker(string imie, string nazwisko, int data_urodzenia)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.data_urodzenia = data_urodzenia;
        }
        public override string ToString()
        {
            return "Imię: " + this.imie + "\nNazwisko: " + this.nazwisko + "\nId: " + this.id + "\nData urodzenia: " + this.data_urodzenia.ToString();
        }

        public static Guid GenerateNewID()
        {
            return System.Guid.NewGuid();
        }

        public int CompareTo(object o)
        {
            Worker w = o as Worker;
            return this.nazwisko.CompareTo(w.nazwisko);
        }

    }

    sealed class OfficeWorker : Worker, IPrintable, IPublicPrintable
    {
        public OfficeWorker(string imie, string nazwisko, int data_urodzenia) : base(imie, nazwisko, data_urodzenia)
        {

        }
    }

    class Manager : Worker, IPrintable, IPublicPrintable
    {
        public Manager(string imie, string nazwisko, int data_urodzenia) : base(imie, nazwisko, data_urodzenia)
        {

        }

        public override string ToString()
        {
            return base.ToString() + "\nManager";
        }
    }

    class Supervisor : Manager, IPrintable, IPublicPrintable
    {
        public Supervisor(string imie, string nazwisko, int data_urodzenia) : base(imie, nazwisko, data_urodzenia)
        {

        }

        public override string ToString()
        {
            return base.ToString().Replace("Manager", "Supervisor");
        }
    }

    public class Office:IEnumerable
    {
        public Worker[] pracownicy = new Worker[6];
        public Office()
        {
            pracownicy[0] = new OfficeWorker("Jan", "Kowalski", 1985);
            pracownicy[1] = new OfficeWorker("Jan", "Kowalewski", 1990);
            pracownicy[2] = new OfficeWorker("Marek", "Kondrat", 1971);
            pracownicy[3] = new Manager("Piotr", "Rogucki", 1980);
            pracownicy[4] = new Supervisor("Jakub", "Czacki", 1986);
            pracownicy[5] = new Manager("Dawid", "Watur", 1976);
        }
        public IEnumerator GetEnumerator()
        {
            return pracownicy.GetEnumerator();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            Office office = new Office();
            Array.Sort(office.pracownicy);
            foreach(var p in office)
            {
                Console.WriteLine(p.ToString());
            }
        }
    }
}