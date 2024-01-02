using EvCreating.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("Event")]
        public int GeselecteerdEvenementId { get; set; }
        [Display(Name = "Naam Evenement")]
        public string? EventNaam { get; set; }
        public Event? GeselecteerdEvenement { get; set; }

        [ForeignKey("EvCreatingUser")]
        public string? EvCreatingUserId { get; set; }
        [Display(Name = "Gebruiker")]
        // ? evCreatingUser is nullable ,de app accepteert null
        public EvCreatingUser? EvCreatingUser { get; set; }

        public EventEvaluation()
            //?? si c'est null alors on prend la valeur à droite
        {
            EvCreatingUserId = Globals.GlobalsUser?.Id ?? "DefaultUserId";
        }
    }
}
