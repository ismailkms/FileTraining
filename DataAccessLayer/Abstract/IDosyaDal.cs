using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IDosyaDal
    {
        void Create(Dosya dosya);
        void Update(Dosya dosya);
        List<Dosya> GetAll();
        Dosya GetById(int id);
        void Delete(Dosya dosya);
    }
}
