using EvCreating.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


public class Mening
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Inhoud is verplicht")]
    public string Inhoud { get; set; }

    public string Naam { get; set; }

    [DataType(DataType.Date)]
    public DateTime Datum { get; set; }

    [Range(1, 5, ErrorMessage = "Rating moet tussen 1 en 5 liggen")]
    public int Rating { get; set; }

    // Extra veld voor de naam van het evenement
    [Display(Name = "Evenement Naam")]
    public string EvenementNaam { get; set; }

    // Navigatie-eigenschap naar het Evenement
    [ForeignKey("Evenement")]
    public int EvenementId { get; set; }
    public Evenement Evenement { get; set; }
}
