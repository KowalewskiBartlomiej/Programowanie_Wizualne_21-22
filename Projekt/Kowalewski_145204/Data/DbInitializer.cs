using Kowalewski_145204.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace Kowalewski_145204.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Producenci.Any())
            {
                return;
            }

            var producenci = new Producent[]
            {
            new Producent{Nazwa = "Skoda", Kraj = "Czechy", RokZalozenia = "1895"},
            new Producent{Nazwa = "Opel", Kraj = "Niemcy", RokZalozenia = "1862"},
            new Producent{Nazwa = "Volkswagen", Kraj = "Niemcy", RokZalozenia = "1938"},
            new Producent{Nazwa = "Citroen", Kraj = "Francja", RokZalozenia = "1919"}
            };
            foreach (Producent p in producenci)
            {
                context.Producenci.Add(p);
            }
            context.SaveChanges();

            var samochody = new Samochod[]
            {
            new Samochod{ProducentID = 1, Nazwa = "Karoq", Nadwozie = Models.Nadwozie.Crossover, TypSkrzyniBiegow = "Automatyczna", Cena = 135800},
            new Samochod{ProducentID = 1, Nazwa = "Fabia", Nadwozie = Models.Nadwozie.Hatchback, TypSkrzyniBiegow = "Manualna", Cena = 70000},
            new Samochod{ProducentID = 1, Nazwa = "Octavia", Nadwozie = Models.Nadwozie.Sedan, TypSkrzyniBiegow = "Automatyczna", Cena = 130000},
            new Samochod{ProducentID = 2, Nazwa = "Corsa", Nadwozie = Models.Nadwozie.Hatchback, TypSkrzyniBiegow = "Manualna", Cena = 80000},
            new Samochod{ProducentID = 2, Nazwa = "Mokka", Nadwozie = Models.Nadwozie.SUV, TypSkrzyniBiegow = "Manualna", Cena = 105000},
            new Samochod{ProducentID = 2, Nazwa = "Insignia", Nadwozie = Models.Nadwozie.Coupe, TypSkrzyniBiegow = "Automatyczna", Cena = 175000},
            new Samochod{ProducentID = 3, Nazwa = "Passat Variant", Nadwozie = Models.Nadwozie.Kombi, TypSkrzyniBiegow = "Automatyczna", Cena = 150000},
            new Samochod{ProducentID = 3, Nazwa = "Golf", Nadwozie = Models.Nadwozie.Hatchback, TypSkrzyniBiegow = "Manualna", Cena = 116000},
            new Samochod{ProducentID = 3, Nazwa = "Tiguan", Nadwozie = Models.Nadwozie.SUV, TypSkrzyniBiegow = "Automatyczna", Cena = 141000},
            new Samochod{ProducentID = 4, Nazwa = "C5 X", Nadwozie = Models.Nadwozie.Hatchback, TypSkrzyniBiegow = "Automatyczna", Cena = 160000},
            new Samochod{ProducentID = 4, Nazwa = "C4 X", Nadwozie = Models.Nadwozie.Hatchback, TypSkrzyniBiegow = "Automatyczna", Cena = 110000},
            new Samochod{ProducentID = 4, Nazwa = "C3", Nadwozie = Models.Nadwozie.Hatchback, TypSkrzyniBiegow = "Manualna", Cena = 80000}
            };
            foreach (Samochod s in samochody)
            {
                context.Samochody.Add(s);
            }
            context.SaveChanges();
        }
    }
}
