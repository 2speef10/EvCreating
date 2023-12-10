using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class FAQQuestion
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Titel is verplicht")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Beschrijving is verplicht")]
    public string Description { get; set; }

    public ICollection<FAQComment> Comments { get; set; }
}