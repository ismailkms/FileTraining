using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class DosyaManager : IDosyaService
    {
        IDosyaDal _dosyaDal;

        public DosyaManager( IDosyaDal dosyaDal )
        {
            _dosyaDal = dosyaDal;
        }

        public void Create(Dosya dosya)
        {
            _dosyaDal.Create(dosya);
        }

        public void Delete(Dosya dosya)
        {
            _dosyaDal.Delete(dosya);
        }

        public List<Dosya> GetAll()
        {
            return _dosyaDal.GetAll();
        }

        public Dosya GetById(int id)
        {
            return _dosyaDal.GetById(id);
        }

        public void Update(Dosya dosya)
        {
            _dosyaDal.Update(dosya);
        }
    }
}
