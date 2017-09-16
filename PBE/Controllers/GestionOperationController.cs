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
    public class GestionOperationController : Controller
    {
        Data.PBETSEntities1 db = new Data.PBETSEntities1();
        // GET: GestionOperation

        public ActionResult Index()
        {
            return View();
        }



        //get-user-bank
        public JsonResult GetDocuments()
        {
            var dbResult = db.TR_USER_BNK.ToList();
            var accounts = (from account in dbResult
                            select new
                            {
                                account.BANK_Code,
                                account.Description
                            });

            return Json(accounts, JsonRequestBehavior.AllowGet);
        }

        //tr groupe utilisateur 
        public JsonResult GetGroupe_Utilisateur()
        {
            var dbResult = db.TR_Groupe_Utilisateur.ToList();
            var users = (from user in dbResult
                         select new
                         {
                             user.Id_Groupe_Utilisateur,
                             user.Libelle_Groupe_Utilisateur
                         });

            return Json(users, JsonRequestBehavior.AllowGet);
        }
        //tr_type_document 
        public JsonResult GetTypes_Document()
        {
            var dbResult = db.TR_Type_Document.ToList();
            var typeusers = (from typeuser in dbResult
                             select new
                             {
                                 typeuser.Id_Type_Document,
                                 typeuser.Code_Type_Document
                             });

            return Json(typeusers, JsonRequestBehavior.AllowGet);
        }
        //get_tr_useroperation

        public JsonResult GetOperation()
        {
            var dbResult = db.TR_Type_Operation.ToList();
            var transactions = (from transaction in dbResult
                                select new
                                {
                                    transaction.Id_op,
                                    transaction.BANK_CODE,
                                    transaction.Code_Operation,
                                    transaction.Libelle_Operation,
                                    transaction.Id_Groupe_Utilisateur,
                                    transaction.Id_Type_Document

                                });

            return Json(transactions, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateOperation(TR_Type_Operation operation)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(operation).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult Delete(int id = 0)
        {
            TR_Type_Operation transaction = db.TR_Type_Operation.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            TR_Type_Operation operation = db.TR_Type_Operation.Find(id);
            db.TR_Type_Operation.Remove(operation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddOperation(TR_Type_Operation transaction)
        {
            if (ModelState.IsValid)
            {
                db.TR_Type_Operation.Add(transaction);
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