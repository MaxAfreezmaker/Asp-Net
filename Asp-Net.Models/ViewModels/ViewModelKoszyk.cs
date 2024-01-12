using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.Models.ViewModels
{
    public class ViewModelKoszyk
    {
        public IEnumerable<Koszyk> Koszyczek { get; set; }
        public double Total { get; set; }
    }
}
