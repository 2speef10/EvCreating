using System;
using System.ComponentModel.DataAnnotations;

namespace EvCreating.Models
{
    public class EventEvaluation
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Naam is verplicht")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Datum van de reactie is verplicht")]
        [DataType(DataType.Date)]
        public DateTime ReactieDatum { get; set; }

        [Required(ErrorMessage = "Waardering is verplicht")]
        [Range(1, 5, ErrorMessage = "Waardering moet tussen 1 en 5 liggen")]
        public int Waardering { get; set; }

        [Required(ErrorMessage = "Inhoud is verplicht")]
        public string Inhoud { get; set; }

        [Display(Name = "Geselecteerd Evenement")]
        public int GeselecteerdEvenementId { get; set; }
        public Event GeselecteerdEvenement { get; set; }
    }
}
