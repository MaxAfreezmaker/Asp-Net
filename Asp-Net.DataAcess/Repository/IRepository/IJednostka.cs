using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.DataAcess.Repository.IRepository
{
    public interface IJednostka
    {
        IKategoria KategoriRI { get; }
        IProdukty ProduktRI { get; }
        IKoszyk KoszykRI { get; }
        IUzytkownik UzytkownikRI { get; }


        void Save();
    }
}
