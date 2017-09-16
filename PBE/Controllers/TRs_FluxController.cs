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
    public class TRs_FluxController : Controller
    {
        Data.PBETSEntities1 db = new Data.PBETSEntities1();

        public JsonResult GetDocuments()
        {
            var dbResult = db.TR_Type_Document.ToList();
            var documents = (from document in dbResult
                                select new
                                {
                                    document.Id_Type_Document,
                                    document.Code_Type_Document
                                });

            return Json(documents, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGroupesUtilisateurs()
        {
            var dbResult = db.TR_Groupe_Utilisateur.ToList();
            var groupesuser = (from groupeuser in dbResult
                             select new
                             {
                                 groupeuser.Id_Groupe_Utilisateur,
                                 groupeuser.Libelle_Groupe_Utilisateur
                             });

            return Json(groupesuser, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBanques()
        {
            var dbResult = db.TR_USER_BNK.ToList();
            var banques = (from banque in dbResult
                             select new
                             {
                                 banque.BANK_Code,
                                 banque.Description
                             });

            return Json(banques, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFlux()
        {
            var dbResult = db.TR_USER_FLUX.ToList();
            var Trs_Flux = (from flux in dbResult
                             select new
                             {
                                 flux.id_User_Flux,
                                 flux.FLOW_CODE,
                                 flux.BANK_CODE,
                                 flux.DESCRIPTION,
                                 flux.I,
                                 flux.FS,
                                 flux.Note,
                                 flux.V,
                                 flux.Electronique,
                                 flux.Delais,
                                 flux.Id_Type_Document,
                                 flux.Id_Groupe_Utilisateur
                             });
            return Json(Trs_Flux, JsonRequestBehavior.AllowGet);
        }
        // GET: TRs_Flux
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateFlux(TR_USER_FLUX flux)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flux).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult Delete(int id = 0)
        {
            TR_USER_FLUX flux = db.TR_USER_FLUX.Find(id);
            if (flux == null)
            {
                return HttpNotFound();
            }
            return View(flux);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            TR_USER_FLUX flux = db.TR_USER_FLUX.Find(id);
            db.TR_USER_FLUX.Remove(flux);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddFlux(TR_USER_FLUX flux)
        {
            if (ModelState.IsValid)
            {
                db.TR_USER_FLUX.Add(flux);
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