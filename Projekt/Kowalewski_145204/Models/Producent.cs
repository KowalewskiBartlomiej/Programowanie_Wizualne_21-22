using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kowalewski_145204.Models
{
    public class Producent
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public string Kraj { get; set; }
        public string RokZalozenia { get; set; }

        public ICollection<Samochod> Samochody { get; set; } = new List<Samochod>();
    }
}
