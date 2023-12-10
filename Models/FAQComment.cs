using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
public class FAQComment
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Inhoud van de reactie is verplicht")]
    public string Content { get; set; }
    public int FAQQuestionId { get; set; }
    public FAQQuestion FAQQuestion { get; set; }
}