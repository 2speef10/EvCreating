using System;

namespace EvCreating.Models
{
    public class EventComment
    {
        public int ID { get; set; }
        public string UserName { get; set; } // Naam van de gebruiker die reageert
        public DateTime ReactieDatum { get; set; }
        public int Waardering { get; set; } // Waardering van 1 tot 5
        public string Inhoud { get; set; } // Inhoud van het commentaar

        public int EventID { get; set; } // ForeignKey naar het Event
        public Event Event { get; set; } // Navigatie-eigenschap naar het Event
    }
}

