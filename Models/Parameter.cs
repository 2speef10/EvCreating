﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvCreating.Models
{
    public class Parameter
    {
        [Key]
        [Display(Name = "Parameter")]
        public string Name { get; set; }

        [Display(Name = "Waarde")]
        public string Value { get; set; }

        [Display(Name = "Beschrijving")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ForeignKey("EvCreatingUser")]
        public string UserId { get; set; }

        public DateTime LastChanged { get; set; } = DateTime.Now;

        public DateTime Obsolete { get; set; } = DateTime.MaxValue;

        public string Destination { get; set; }
    }
}