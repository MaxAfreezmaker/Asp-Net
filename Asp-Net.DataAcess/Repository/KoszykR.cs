using Asp_Net.DataAcess.Repository.IRepository;
using Asp_Net.Models;
using Asp_Net.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.DataAcess.Repository
{
    public class KoszykR : RepozytoriumR<Models.ViewModels.Koszyk>, IKoszyk
    {
        private ApplicationDbContext _db;

        public KoszykR(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementCount(Models.ViewModels.Koszyk shoppingCart, int Count)
        {
            shoppingCart.Ilosc -= Count;
            return shoppingCart.Ilosc;
        }

        public int IncrementCount(Models.ViewModels.Koszyk shoppingCart, int Count)
        {
            shoppingCart.Ilosc += Count;
            return shoppingCart.Ilosc;
        }
    }
}
