using Asp_Net.DataAcess.Repository.IRepository;
using Asp_Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.DataAcess.Repository
{
    public class ProduktyR: RepozytoriumR<Produkt>, IProdukty
    {
        private ApplicationDbContext _db;

        public ProduktyR(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Produkt item)
        {
            var itemFromDb = _db.Produkty.FirstOrDefault(x => x.Id == item.Id);
            if (itemFromDb != null)
            {
                itemFromDb.Nazwa = item.Nazwa;
                itemFromDb.Opis = item.Opis;
                itemFromDb.Pochodzenie = item.Pochodzenie;
                itemFromDb.ListaCen = item.ListaCen;
                itemFromDb.Cena = item.Cena;
                itemFromDb.Od50 = item.Od50;
                itemFromDb.Od100 = item.Od100;
                itemFromDb.KategoriaId = item.KategoriaId;

                if (item.ImageUrl != null)
                {
                    itemFromDb.ImageUrl = item.ImageUrl;
                }              
            }
        }
    }
}
