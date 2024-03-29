﻿using EvCreating.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvCreating.Models
{
    public class EventEvaluation
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Veld is leeg")]
        [Display(Name = "Naam")]
        public string Naam { get; set; }

        [Display(Name = "Reactie datum")]
        [Required(ErrorMessage = "Veld is leeg")]
        [DataType(DataType.Date)]
        public DateTime ReactieDatum { get; set; }

        [Display(Name = "Waardering")]
        [Required(ErrorMessage = "Veld is leeg")]
        [Range(1, 5, ErrorMessage = "Waardering moet tussen 1 en 5 liggen")]
        public int Waardering { get; set; }

        [Display(Name = "Inhoud")]
        [Required(ErrorMessage = "Veld is leeg")]
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
