namespace Laboratorium2
{

    abstract class Worker
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public System.Guid id = System.Guid.NewGuid();
        public string data_urodzenia { get; set; }

        public Worker() { }

        public Worker(string imie, string nazwisko, string data_urodzenia)
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

    }

    sealed class OfficeWorker:Worker
    {
        public OfficeWorker(string imie, string nazwisko, string data_urodzenia) : base(imie, nazwisko, data_urodzenia)
        {

        }
    }

    class Manager : Worker
    {
        public Manager(string imie, string nazwisko, string data_urodzenia) : base(imie, nazwisko, data_urodzenia)
        {

        }

        public override string ToString()
        {
            return base.ToString() + "\nManager";
        }
    }

    class Supervisor : Manager
    {
        public Supervisor(string imie, string nazwisko, string data_urodzenia) : base(imie, nazwisko, data_urodzenia)
        {

        }

        public override string ToString()
        {
            return base.ToString().Replace("Manager", "Supervisor");
        }
    }

    /*readonly struct Point
    {
        /*static Point()
        {
            Point p = new Point();
        }

        public int X { get; set; }
        public int Y { get; set; }

        public readonly int V { get; init; }

    }*/

    struct Pracownik
    {
        public string Imie;
        public string Nazwisko;
        public Pracownik(string imie)
        {
            Imie = imie;
            Nazwisko = "Nowak";
        }
        public void Print()
        {
            Console.WriteLine($"{Imie} {Nazwisko}");
        }
    }
    /*
    public readonly struct Rect
    {
        public int Width { get; }
        public int Height { get; set; } //error
    }
    public struct Circle
    {
        public int Radius;
        public readonly int Position { get; }
    }*/

    public class Osoba
    {
        public string imie;
        public string nazwisko;
        public int wiek;
        public Osoba(string imie, string nazwisko, int wiek)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.wiek = wiek;
            Console.WriteLine("ctor 1");
        }
        public Osoba(string imie, string nazwisko) : this(imie, nazwisko, 0)
        {
            Console.WriteLine("ctor 2");
        }
        public Osoba(int wiek) : this("", "", wiek)
        {
            Console.WriteLine("ctor 3");
        }
    }

    /*public class Point
    {
        public int X;
        public int Y;

        public static void DodajX(Point p) => p.X++;
        public static void DodajXref(ref Point p) => p.X++;
        public static void NowyPunkt(Point p) => p = new Point();
        public static void NowyPunktref(ref Point p) => p = new Point();
    }*/
    public struct Point
    {
        public int X;
        public int Y;

        public static void DodajX(Point p) => p.X++;
        public static void DodajXref(ref Point p) => p.X++;
        public static void NowyPunkt(Point p) => p = new Point();
        public static void NowyPunktref(ref Point p) => p = new Point();
    }

    public struct Point3
    {
        public int X;
        public int Y;
    }


    internal class Program
    {
        public static void DodajX(Point3 p) => p.X++;
        public static void NowyPunktref(ref Point3 p) => p = new Point3();

        static int Dodaj(ref int x, int y)
        {
            return x + y;
        }

        static void Main(string[] args)
        {
            //ZADANIE 1
            /*bool x;

            if (x)
            {
                Console.WriteLine("Hello, World!");
            }*/

            //ZADANIA 2-7
            /*OfficeWorker worker = new OfficeWorker("Jan", "Nowak", "28-04-2000") { imie = "Bartek"};
            Console.WriteLine(worker.ToString());*/

            //ZADANIE 8
            /*Manager worker = new Manager("Jan", "Nowak", "28-04-2000") { imie = "Bartek" };
            Console.WriteLine(worker.ToString());*/

            //ZADANIE 9
            /*Supervisor worker = new Supervisor("Jan", "Nowak", "28-04-2000") { imie = "Bartek" };
            Console.WriteLine(worker.ToString());*/

            //ZADANIE 11
            /*Point p = new Point();*/

            //ZADANIE 12


            //ZADANIE 13
            /*(var Imie, var Nazwisko, var wiek)  = ("Jan", "Nowak", 33);
            var (Imie2, Nazwisko2, wiek2) = ("Jan", "Nowak", 33);
            var pracownik = ("Jan", "Nowak", 33);
            (string Imie, string Nazwisko, int Wiek) prac = ("Jan", "Nowak",
            33);
            Console.WriteLine(prac.Wiek);
            Console.WriteLine(pracownik.Item1);*/

            //Przykład ze strukturami z wykładu
            /*
            Pracownik pracownik;
            pracownik.Imie = "Jan"; 
            pracownik.Nazwisko = "Kowalski";
            Pracownik p = new Pracownik();
            Pracownik p2 = new(); //C#9
            Console.WriteLine("default constructor {0}", p.Nazwisko);
            p.Print();
            p2.Print();
            pracownik.Print(); //ok po odkomentowaniu dwóch linii wyżej
            */

            //KLASA - TYP REFERENCYJNY, ALE JEŻELI NIE PRZEKAZUJESZ Z REF TO Z REFERENCJą przekazują się tylko parametry klasy
            //DOPIERO JAK PRZEKAZESZ Z REF TO JEST TO REFERENCJA DO OBIEKTU KLASY

            //STRUKTURA - TYP WARTOSCIOWY, BEZ REF UTWORZY KOPIE I JA ZINKREMENTUJE A NIE ORYGINAL 
            //PRZEKAZUJAC Z REF ZINKREMENTUJE ORYGINAŁ, TO SAMO Z UTWORZENIEM NOWEGO OBIEKTU

            /*Point punkt = new Point();
            Console.WriteLine($"({punkt.X}:{punkt.Y})");
            Point.DodajX(punkt);
            Console.WriteLine($"1: ({punkt.X}:{punkt.Y})");
            Point.DodajXref(ref punkt);
            Console.WriteLine($"2: ({punkt.X}:{punkt.Y})");
            Point.NowyPunkt(punkt);
            Console.WriteLine($"3: ({punkt.X}:{punkt.Y})");
            Point.NowyPunktref(ref punkt);
            Console.WriteLine($"4: ({punkt.X}:{punkt.Y})");*/

            //Osoba osoba = new Osoba(20);  ctor1, ctor3
            //Osoba o = new Osoba(imie: "Jurek", nazwisko: "Nowak", wiek: 20); MOZNA

            Nullable<int> i = 0;
            bool odp = false;

            if (i.HasValue)
            {
                odp = i ?? false;
            }
            Console.WriteLine(odp);
        }
    }
}