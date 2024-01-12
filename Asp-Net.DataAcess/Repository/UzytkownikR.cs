using Asp_Net.DataAcess.Repository.IRepository;
using Asp_Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net.DataAcess.Repository
{
    public class UzytkownikR : RepozytoriumR<UzytkownikDane>, IUzytkownik
    {
        private ApplicationDbContext _db;

        public UzytkownikR(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
