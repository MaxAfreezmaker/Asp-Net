using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.Models.ViewModels
{
    public class Koszyk
    {
        public int Id { get; set; }
        public int ProduktId { get; set; }
        [ForeignKey("ProduktId")]
        [ValidateNever]
        public Produkt Produkt { get; set; }
        [Range(1,1000,ErrorMessage="Proszę wprowadzić wartość od 1 do 1000")]
        public int Ilosc { get; set; }

        public string IdUzytkownik { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public UzytkownikDane Uzytkownik { get; set; }
        [NotMapped]
        public double Cena { get; set; }
    }
}
