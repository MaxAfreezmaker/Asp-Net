using Asp_Net.Models;
using Asp_Net.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.DataAcess.Repository.IRepository
{
    public interface IKoszyk : IRepozytorium<Models.ViewModels.Koszyk>
    {
        int IncrementCount(Models.ViewModels.Koszyk shoppingCart, int Count);
        int DecrementCount(Models.ViewModels.Koszyk shoppingCart, int Count);
    }
}
