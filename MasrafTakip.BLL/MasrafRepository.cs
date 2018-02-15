using MasrafTakip.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasrafTakip.BLL;

namespace MasrafTakip.BLL
{
   public class MasrafRepository : Irepository<Masraflar>

    {
        MasrafKayitEntities db = new MasrafKayitEntities();
        public void Delete(int itemID)
        {
            db.Masraflar.Remove(db.Masraflar.Find(itemID));
            db.SaveChanges();
        }

        public void Insert(Masraflar item)
        {
            db.Masraflar.Add(item);
            db.SaveChanges();
        }

        public List<Masraflar> selectAll()
        {
           return db.Masraflar.ToList();
        }
        public List<Masraflar> selectAllcalisan()
        {
            return db.Masraflar.ToList();
        }
       
        public List<Masraflar> selectbyperid(int personelID)
        {
            return db.Masraflar.Where(f => f.PersonelID == personelID).ToList();

        }

        public void Update(Masraflar item)
        {
            Masraflar guncel = db.Masraflar.Find(item.MasrafID);
            db.Entry(guncel).CurrentValues.SetValues(item);
            db.SaveChanges();

        }
        
       
    }
}
