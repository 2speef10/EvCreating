using System.ComponentModel.DataAnnotations;

namespace EvCreating.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public DateTime Datum { get; set; }
        public string Locatie { get; set; }
        public string Beschrijving { get; set; }
        public string Soort { get; set; }
        public bool IsDeleted { get; set; }
        

    }
}
