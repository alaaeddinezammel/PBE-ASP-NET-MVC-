using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBE.Data;

namespace PBE.Controllers
{
    public class GestionUserGroupeController : Controller
    {
        private PBETSEntities1 db = new PBETSEntities1();

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetUserGroupe()
        {

                var dbResult = db.TJ_USER_GROUPE.ToList();
                var ugroupe = (from ugroup in dbResult
                               select new
                               {

                                   ugroup.id_User_Group,
                                   ugroup.Matricule,
                                   ugroup.Id_Groupe_Utilisateur,
                                   ugroup.CMP_CODE
                               });
                return Json(ugroupe, JsonRequestBehavior.AllowGet);
            }
        [HttpGet]
       public JsonResult GetUserGroupeByCompany(string cmp_code)
        {
           // cmp_code = "4040";
            if(cmp_code == "0")
            {
                var dbResult = db.TJ_USER_GROUPE.ToList();
                var ugroupe = (from ugroup in dbResult
                               select new
                               {

                                   ugroup.id_User_Group,
                                   ugroup.Matricule,
                                   ugroup.Id_Groupe_Utilisateur,
                                   ugroup.CMP_CODE
                               });
                return Json(ugroupe, JsonRequestBehavior.AllowGet);
            }
                 
            else
            {
                var dbResult = db.TJ_USER_GROUPE.Where(pr => pr.CMP_CODE == cmp_code);

                var ugroupe = (from ugroup in dbResult
                               select new
                               {

                                   ugroup.id_User_Group,
                                   ugroup.Matricule,
                                   ugroup.Id_Groupe_Utilisateur,
                                   ugroup.CMP_CODE
                               });
                return Json(ugroupe, JsonRequestBehavior.AllowGet);
            }
                 
        }

        public JsonResult GetGroupeUsers()
        {
            var dbResult = db.TR_Groupe_Utilisateur.ToList();
            var groupeusers = (from groupe in dbResult
                               select new
                               {

                                   groupe.Id_Groupe_Utilisateur,
                                   groupe.Libelle_Groupe_Utilisateur,
                                   
                               });

            return Json(groupeusers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsers()
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
        public JsonResult GetCMP()
        {
            var dbResult = db.COMPANIES.ToList();
            var cmps = (from cmp in dbResult
                         select new
                         {

                             cmp.CMP_CODE,
                             cmp.DESCRIPTION
                             

                         });

            return Json(cmps, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateUserGroupe(TJ_USER_GROUPE ugroupe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ugroupe).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult Delete(int id = 0)
        {
            TJ_USER_GROUPE ugroupe = db.TJ_USER_GROUPE.Find(id);
            if (ugroupe == null)
            {
                return HttpNotFound();
            }
            return View(ugroupe);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            TJ_USER_GROUPE ugroupe = db.TJ_USER_GROUPE.Find(id);
            db.TJ_USER_GROUPE.Remove(ugroupe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddUserGroupe(TJ_USER_GROUPE ugroupe)
        {
            if (ModelState.IsValid)
            {
                db.TJ_USER_GROUPE.Add(ugroupe);
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }



        // Groupe Utilisateurs : 

        public JsonResult GetGroupeUtilisateurs()
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

        [HttpGet]
        public ActionResult NbUsers(int id,string cmp_code)
        {
            var dbResult = db.TJ_USER_GROUPE.Where(pr => pr.Id_Groupe_Utilisateur == id && pr.CMP_CODE == cmp_code);
            var Nbusrs = (from user in dbResult
                          select new
                          {
                              user.Matricule
                          });
            return Json(Nbusrs.Distinct().Count(), JsonRequestBehavior.AllowGet);
        }

        public int NbUser(int id , string cmp_code)
        {
            var dbResult = db.TJ_USER_GROUPE.Where(pr => pr.Id_Groupe_Utilisateur == id && pr.CMP_CODE == cmp_code);
            var Nbusrs = (from user in dbResult
                          select new
                          {
                              user.Matricule
                          });
            return (Nbusrs.Distinct().Count());
        }

        public ActionResult DeleteGroupe(int id = 0)
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
        public ActionResult DeleteConfirmedGroupe(int id)
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
