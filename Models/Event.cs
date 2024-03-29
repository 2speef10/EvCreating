﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EvCreating.Models
{
    
public class Event
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Veld is leeg")]
        [Display(Name = "Naam")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Veld is leeg")]
        [Display(Name = "Datum")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Veld is leeg")]
        [Display(Name = "Locatie")]
        public string Locatie { get; set; }

        [Required(ErrorMessage = "Veld is leeg")]
        [Display(Name = "Beschrijving")]
        public string Beschrijving { get; set; }

        [Required(ErrorMessage = "Veld is leeg")]
        [Display(Name = "Soort")]
        public string Soort { get; set; }

        [Display(Name = "Is Verwijderd")]
        public bool IsDeleted { get; set; }
    }


    // Nieuwe eigenschap toevoegen om het aantal bezoeken bij te houden
    //public int VisitCount { get; set; }
}

