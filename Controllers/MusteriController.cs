using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProjem.Models.Entity;

namespace MvcStokProjem.Controllers
{
    public class MusteriController : Controller
    {
        // GET: MUSTERI
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBL_MUSTERI select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.TBL_MUSTERI.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TBL_MUSTERI p2)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBL_MUSTERI.Add(p2);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var musteri = db.TBL_MUSTERI.Find(id);
            db.TBL_MUSTERI.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TBL_MUSTERI.Find(id);
            return View("MusteriGetir", musteri);
        }
        public ActionResult Guncelle (TBL_MUSTERI p1)
        {
            var musteri = db.TBL_MUSTERI.Find(p1.MUSTERIID);
            musteri.MUSTERIAD = p1.MUSTERIAD;
            musteri.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}