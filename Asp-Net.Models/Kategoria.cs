using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asp_Net.Models
{
    public class Kategoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nazwa { get; set; }

        [DisplayName("Wyświetlanie zamówienia")]
        [Range(1,100,ErrorMessage = "Kolejność wyświetlania musi być pomiędzy 1 a 100")]
        public int KolejnoscWyswietlania { get; set; }

        public DateTime DataUtworzenia { get; set; } = DateTime.Now;
    }
}
