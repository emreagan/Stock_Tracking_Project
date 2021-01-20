﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProjem.Models.Entity;

namespace MvcStokProjem.Controllers
{

    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBL_URUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList() select new SelectListItem 
                                             { Text = i.KATEGORIAD, 
                                               Value = i.KATEGORIID.ToString() 
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TBL_URUNLER p3)
        {
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p3.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            p3.TBL_KATEGORILER = ktg;
            db.TBL_URUNLER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.TBL_URUNLER.Find(id);
            db.TBL_URUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBL_URUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("Urungetir", urun);
        }
        public ActionResult Guncelle(TBL_URUNLER p1)
        {
            var urun = db.TBL_URUNLER.Find(p1.URUNID);
            urun.URUNID = p1.URUNID;
            urun.URUNAD = p1.URUNAD;
            urun.MARKA = p1.MARKA;
            urun.FIYAT = p1.FIYAT;
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p1.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            urun.STOK = p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}