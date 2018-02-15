using MasrafTakip.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakip.BLL
    
{
   
  public  class KullaniciRepository : Irepository<Personeller>
    {
        MasrafKayitEntities db = new MasrafKayitEntities();
        public void Delete(int itemID)
        {
            throw new NotImplementedException();
        }

        public void Insert(Personeller item)
        {
            throw new NotImplementedException();
        }

        public List<Personeller> selectAll()
        {
            return db.Personeller.ToList();
        }

        public void Update(Personeller item)
        {
            throw new NotImplementedException();
        }
    }
}
