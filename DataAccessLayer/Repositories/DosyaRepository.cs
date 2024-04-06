using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repisitories
{
    public class DosyaRepository : IDosyaDal
    {
        DosyaDbContext _context;

        public DosyaRepository(DosyaDbContext context) 
        { 
            _context = context;
        }

        public void Create(Dosya dosya)
        {
            _context.Dosyalar.Add(dosya);
            _context.SaveChanges();
        }

        public void Delete(Dosya dosya)
        {
            _context.Dosyalar.Remove(dosya);
            _context.SaveChanges();
        }

        public List<Dosya> GetAll()
        {
            return _context.Dosyalar.ToList();
        }

        public Dosya GetById(int id)
        {
            return _context.Dosyalar.Find(id);
        }

        public void Update(Dosya dosya)
        {
            _context.Dosyalar.Update(dosya);
            _context.SaveChanges();
        }
    }
}
