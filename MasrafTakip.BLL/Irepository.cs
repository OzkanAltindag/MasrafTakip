using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakip.BLL
{
   public interface Irepository<T>
    {
        void Insert(T item);
        void Update(T item);
        void Delete(int itemID);
        List<T> selectAll();
    }
}
