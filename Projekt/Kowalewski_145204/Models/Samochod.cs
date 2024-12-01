namespace Kowalewski_145204.Models
{
    public enum Nadwozie
    {
        Cabriolet, Coupe, Crossover, Hatchback, Kombi, Liftback, Roadster, Sedan, SUV, Van
    }
    public class Samochod
    {
        public int ID { get; set; }

        public int ProducentID { get; set; }

        public string Nazwa { get; set; }

        public Nadwozie Nadwozie { get; set; }

        public string TypSkrzyniBiegow { get; set; }

        public int Cena { get; set; }

        public Producent? Producent { get; set; }
    }
}
