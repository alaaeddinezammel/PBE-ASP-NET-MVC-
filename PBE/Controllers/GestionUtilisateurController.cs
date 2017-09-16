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
    public class GestionUtilisateurController : Controller
    {
        Data.PBETSEntities1 db = new Data.PBETSEntities1();

        // GET: GestionBanques
        public ActionResult Index()
        {
            return View();
        }

        //Utilisateur Exist ou nn 
 
        public JsonResult NbUsers(string Matricule)
        {
            var dbResult = db.Param_Login.Where(pr => pr.Matricule == Matricule);
                var user = (from u in dbResult
                               select new
                               {
                                   u.Matricule
                               });
                return Json(user.Count(), JsonRequestBehavior.AllowGet);
            }

        public JsonResult GetUsers()
        {
            var dbResult = db.Param_User.ToList();
            var users = (from user in dbResult
                           select new
                           {

                               user.Matricule,
                               user.Identite,
                               user.Code_Filiale,
                               user.Date_Mod,
                               user.Compte_LOTUS,
                               user.CIN_Resp,
                               user.Est_PDG,
                               user.Est_Administrateur,
                               user.Est_Centralisateur,
                               user.Est_Directeur
                               
                           });

            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateUser(Param_User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult Delete(string id = null)
        {
            Param_User banque = db.Param_User.Find(id);
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
            Param_User user = db.Param_User.Find(id);
            db.Param_User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddUser(Param_User user)
        {
            if (ModelState.IsValid)
            {
                db.Param_User.Add(user);
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
        public JsonResult GetUsersLogin()
        {
            var dbResult = db.Param_Login.ToList();
            var users = (from user in dbResult
                         select new
                         {
                             user.UserId,
                             user.Matricule,
                             user.Login_User,
                             user.Mpt_User,
                             user.Est_Active,
                             user.Date_Mod,
                             user.User_Id,
                             user.EstPDG,
                             user.Est_Administrateur,
                             user.Est_Centralisateur,
                             user.Departement,
                             user.Old_Id_User,
                             user.Est_Directeur

                         });

            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateUserLogin(Param_Login user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult DeleteUserLogin(int id = 0)
        {
            Param_Login userLogin = db.Param_Login.Find(id);
            if (userLogin == null)
            {
                return HttpNotFound();
            }
            return View(userLogin);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirmedUserLogin(int id)
        {
            Param_Login userLogin = db.Param_Login.Find(id);
            db.Param_Login.Remove(userLogin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddUserLogin(Param_Login user)
        {
            var nbUser = db.Param_Login.Where(pr => pr.Matricule == user.Matricule && pr.Login_User == user.Matricule);
            if (nbUser.Count() > 0)
            {
                return Json("false");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Param_Login.Add(user);
                    db.SaveChanges();
                    return Json("true");
                }
                return Json("false");
            }
         
            
        }
    }
}