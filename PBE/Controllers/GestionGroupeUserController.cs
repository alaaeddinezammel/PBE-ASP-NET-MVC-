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
    public class GestionGroupeUserController : Controller
    {
        Data.PBETSEntities1 db = new Data.PBETSEntities1();

        // GET: GestionBanques
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetGroupeUsers()
        {
            var dbResult = db.TR_Groupe_Utilisateur.ToList();
            var groupeusers = (from groupe in dbResult
                           select new
                           {

                               groupe.Id_Groupe_Utilisateur,
                               groupe.Libelle_Groupe_Utilisateur,
                               groupe.Description_Role
                           });

            return Json(groupeusers, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateGroupeUsers(TR_Groupe_Utilisateur groupe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupe).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult Delete(int id = 0)
        {
            TR_Groupe_Utilisateur groupe = db.TR_Groupe_Utilisateur.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            TR_Groupe_Utilisateur groupe = db.TR_Groupe_Utilisateur.Find(id);
            db.TR_Groupe_Utilisateur.Remove(groupe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddGroupeUsers(TR_Groupe_Utilisateur groupe)
        {
            if (ModelState.IsValid)
            {
                db.TR_Groupe_Utilisateur.Add(groupe);
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