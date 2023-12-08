using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace EvCreating.Models
{
    public class Evenement
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public DateTime Datum { get; set; }
        public string Locatie { get; set; }
        public string Beschrijving { get; set; }
        public string Soort { get; set; }

        // Navigatie-eigenschap voor Meningen die aan dit evenement zijn gekoppeld
        public ICollection<Mening> Meningen { get; set; }
    }
}

