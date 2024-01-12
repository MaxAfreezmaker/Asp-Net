using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.Models
{
    public class Produkt
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public string Pochodzenie { get; set; }
        [Required]
        [Range(1,10000)]
        public double ListaCen { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Ceny od 1-50")]
        public double Cena { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Ceny od 50-100")]
        public double Od50 { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Ceny od 100+")]
        public double Od100 { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name="Kategoria")]
        public int KategoriaId { get; set; }
        [ValidateNever]
        public Kategoria Kategoria { get; set; }



    }
}
