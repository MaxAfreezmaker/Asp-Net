using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.Models.ViewModels
{
    public class ViewModelProdukt
    {
        public Produkt Produkt { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> listaKategorii { get; set; }   
    }
}
