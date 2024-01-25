using System;
using System.ComponentModel.DataAnnotations;

namespace EvCreating.Models
{
    
public class Event
    {
        public int ID { get; set; }

        [Display(Name = "Naam")]
        public string Naam { get; set; }

        [Display(Name = "Datum")]
        public DateTime Datum { get; set; }

        [Display(Name = "Locatie")]
        public string Locatie { get; set; }

        [Display(Name = "Beschrijving")]
        public string Beschrijving { get; set; }

        [Display(Name = "Soort")]
        public string Soort { get; set; }

        [Display(Name = "Is Verwijderd")]
        public bool IsDeleted { get; set; }
    }


    // Nieuwe eigenschap toevoegen om het aantal bezoeken bij te houden
    //public int VisitCount { get; set; }
}

