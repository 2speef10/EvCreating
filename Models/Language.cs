﻿using EvCreating.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EvCreating.Models
{
    public class Language
    {
        public static List<Language> Languages { get; set; }
        public static Dictionary<string, Language> LanguagesById { get; set; }


        [Key]
        [Display(Name = "Code")]
        [MaxLength(2)]
        [MinLength(2)]
        public string Id { get; set; }

        [Display(Name = "Taal")]
        public string Name { get; set; }

        [Display(Name = "Systeemtaal?")]
        public bool IsSystemLanguage { get; set; }

        [Display(Name = "Beschikbaar vanaf")]
        public DateTime IsAvailable { get; set; } = DateTime.Now;


        public static void GetLanguages(EvCreatingContext context)
        {
            Languages = context.Language.Where(lan => lan.IsAvailable < DateTime.Now).OrderBy(lan => lan.Name).ToList();
            LanguagesById = new Dictionary<string, Language>();
            foreach (Language language in Languages)
            {
                LanguagesById[language.Id] = language;
            }
        }
    }
}