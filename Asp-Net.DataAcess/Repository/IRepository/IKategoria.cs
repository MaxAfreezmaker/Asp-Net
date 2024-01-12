using Asp_Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.DataAcess.Repository.IRepository
{
    public interface IKategoria : IRepozytorium<Kategoria>
    {
        void Update (Kategoria item);
    }
}
