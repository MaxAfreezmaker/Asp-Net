using Asp_Net.DataAcess.Repository.IRepository;
using Asp_Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.DataAcess.Repository
{
    public class KategoriaR : RepozytoriumR<Kategoria>, IKategoria
    {
        private ApplicationDbContext _db;

        public KategoriaR(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Kategoria item)
        {
            _db.Kategorie.Update(item);
        }
    }
}
