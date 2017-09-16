using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using PBE.Models;
using PBE.Data;

namespace PBE.Controllers
{
    public class GestionBanquesController : Controller
    {
        Data.PBETSEntities1 db = new Data.PBETSEntities1();

        // GET: GestionBanques
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetBanques()
        {
            var dbResult = db.TR_USER_BNK.ToList();
            var banques = (from banque in dbResult
                           select new
                           {

                               banque.BANK_Code,
                               banque.Description,
                               banque.Electronique
                           });

            return Json(banques, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateBanque(TR_USER_BNK banque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banque).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult Delete(string id = null)
        {
            TR_USER_BNK banque = db.TR_USER_BNK.Find(id);
            if (banque == null)
            {
                return HttpNotFound();
            }
            return View(banque);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirmed(string id)
        {
            TR_USER_BNK banque = db.TR_USER_BNK.Find(id);
            db.TR_USER_BNK.Remove(banque);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddBanque(TR_USER_BNK banque)
        {
            if (ModelState.IsValid)
            {
                db.TR_USER_BNK.Add(banque);
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}