using Asp_Net.DataAcess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.DataAcess.Repository
{
    public class JednostkaR : IJednostka
    {
        private ApplicationDbContext _db;

        public JednostkaR(ApplicationDbContext db)
        {
            _db = db;
           KategoriRI = new KategoriaR(_db);
           ProduktRI = new ProduktyR(_db);
           KoszykRI = new KoszykR(_db);
           UzytkownikRI = new UzytkownikR(_db);
        }
         
        public IKategoria KategoriRI { get; private set; }
        public IProdukty ProduktRI { get; private set; }
        public IKoszyk KoszykRI { get; private set; }
        public IUzytkownik UzytkownikRI { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
