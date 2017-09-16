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
    public class GestionUser_CompteController : Controller
    {
        Data.PBETSEntities1 db = new Data.PBETSEntities1();

        // GET: GestionBanques
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAccounts()
        {
            var dbResult = db.ACCOUNTS.ToList();
            var accounts = (from account in dbResult
                            select new
                            {
                                account.ACC_CODE
                            });

            return Json(accounts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParam_Users()
        {
            var dbResult = db.Param_User.ToList();
            var users = (from user in dbResult
                         select new
                         {
                             user.Matricule,
                             user.Identite
                         });

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public string GetIdentite(string Matricule)
        {
            //Matricule = "00007150";
            //var dbResult = db.Param_User.ToList();
            Param_User usercompte = db.Param_User.Find(Matricule);
            //var dbResult = db.Param_User.Where(pr => pr.Matricule == Matricule);
            /* var users = (from user in dbResult
                          select new
                          {
                              user.Identite
                          });*/
            return usercompte.Identite;

            //return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTypes_Users()
        {
            var dbResult = db.TR_USER_Type.ToList();
            var typeusers = (from typeuser in dbResult
                             select new
                             {
                                 typeuser.Id_Type_Utilisateur,
                                 typeuser.Libelle_Type_Utilisateur
                             });

            return Json(typeusers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetComptesUtilisateurs()
        {
            var dbResult = db.TR_USER_COMPTE.ToList();
            var usercomptes = (from usercompte in dbResult
                               select new
                               {

                                   usercompte.id_User_Compte,
                                   usercompte.Matricule,
                                   usercompte.ACC_CODE,
                                   usercompte.Id_Type_Utilisateur
                               });

            return Json(usercomptes, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateUserCompte(TR_USER_COMPTE usercompte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usercompte).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult Delete(int id = 0)
        {
            TR_USER_COMPTE usercompte = db.TR_USER_COMPTE.Find(id);
            if (usercompte == null)
            {
                return HttpNotFound();
            }
            return View(usercompte);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            TR_USER_COMPTE usercompte = db.TR_USER_COMPTE.Find(id);
            db.TR_USER_COMPTE.Remove(usercompte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddUserCompte(TR_USER_COMPTE usercompte)
        {
            if (ModelState.IsValid)
            {
                db.TR_USER_COMPTE.Add(usercompte);
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