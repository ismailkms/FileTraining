using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        IDosyaService _dosyaService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public HomeController(IDosyaService dosyaService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _dosyaService = dosyaService;
            _environment = environment;
        }

        public IActionResult Index()
        {
            List<Dosya> dosya = _dosyaService.GetAll();
            return View(dosya);
        }

        [HttpGet]
        public IActionResult AddDosya()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(2*1024*1024)]
        //Burada maximum girilebilecek dosya boyutunu ayarladık
        //Değreler byte tipinde tutuluyor
        //1024 byte 1 KiloByte, 1024 KiloByte 1 MegaByte'dır
        //Bundan dolayı 2*1024*1024 şeklinde yaptığımız tanımlamada 2mb'lık bir sınır koymuş oluruz
        public IActionResult AddDosya(IFormFile Getfile)
        {
            if (Getfile != null)
            {
                AddDosyaFunc(Getfile);

                Dosya dosya = DosyaFunc(Getfile);

                _dosyaService.Create(dosya);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UpdateDosya(int id)
        {
            Dosya dosya = _dosyaService.GetById(id);
            return View(dosya);
        }

        [HttpPost]
        [RequestSizeLimit(2*1024*1024)]
        //Burada maximum girilebilecek dosya boyutunu ayarladık
        //Değreler byte tipinde tutuluyor
        //1024 byte 1 KiloByte, 1024 KiloByte 1 MegaByte'dır
        //Bundan dolayı 2*1024*1024 şeklinde yaptığımız tanımlamada 2mb'lık bir sınır koymuş oluruz
        public IActionResult UpdateDosya(IFormFile Getfile, int id)
        {

            if (Getfile != null)
            {
                Dosya dosya = _dosyaService.GetById(id);
                if (dosya != null)
                {
                    DeleteDosyaFunc(id);

                    AddDosyaFunc(Getfile);

                    Dosya dosyaFunc = DosyaFunc(Getfile);
                    dosya.Ad = dosyaFunc.Ad;
                    dosya.DosyaTipi = dosyaFunc.DosyaTipi;
                    dosya.Boyut = dosyaFunc.Boyut;
                    _dosyaService.Update(dosya);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteDosya(int id)
        {
            Dosya dosya = _dosyaService.GetById(id);
            if (dosya != null)
            {
                DeleteDosyaFunc(id);
                _dosyaService.Delete(dosya);
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult DownloadDosya(int id)
        {
            Dosya dosya = _dosyaService.GetById(id);
            if (dosya != null)
            {
                string fileName = dosya.Ad + dosya.DosyaTipi;
                string filePath = Path.Combine(_environment.WebRootPath, "Dosyalar", fileName);
                //filePath e dosya yolunu çektik

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                return File(fileBytes, "application/force-download", fileName);
            }

            return RedirectToAction("Index", "Home");
        }

        public void AddDosyaFunc(IFormFile file)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "Dosyalar", file.FileName);
            //filePath e dosya yolunu çektik
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            //using'i kullanmamızın sebebi dosyada işlem yapıldıktan sonra dosyanın kapatılmasını istememizdir
            //Dosya açık bırakıldığı takdirde update,delete vb. işlemlerde dosya başka bir yerde kullanılıyor diye hata verecektir
        }

        public void DeleteDosyaFunc(int id)
        {
            Dosya dosya = _dosyaService.GetById(id);
            if (dosya != null)
            {
                string filePath = Path.Combine(_environment.WebRootPath, "Dosyalar", dosya.Ad + dosya.DosyaTipi);
                //filePath e dosya yolunu çektik
                System.IO.File.Delete(filePath);
            }
        }

        public Dosya DosyaFunc(IFormFile file)
        {
            string filePath = Path.Combine(_environment.WebRootPath, "Dosyalar", file.FileName);
            //filePath e dosya yolunu çektik
            var name = Path.GetFileNameWithoutExtension(file.FileName);
            //Extension(Dosya uzantısı) olmadan dosya adını çektik
            var extension = Path.GetExtension(file.FileName);
            //Extension(Dosya uzantısı) bilgisini çektik
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            //Dosya boyutunu çektik
            Dosya dosya = new() {
                Ad= name,
                DosyaTipi=extension,
                Boyut=fileBytes
            };
            return dosya;
        }

    }
}