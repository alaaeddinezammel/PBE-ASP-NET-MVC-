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
    public class GestionDocumentController : Controller
    {
        Data.PBETSEntities1 db = new Data.PBETSEntities1();

        // GET: GestionBanques
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTransactions()
        {
            var dbResult = db.TR_Type_Transaction.ToList();
            var transactions = (from transaction in dbResult
                                select new
                                {
                                    transaction.Id_Type_Transaction,
                                    transaction.Libelle_Type_Transaction
                                });

            return Json(transactions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocuments()
        {
            var dbResult = db.TR_Type_Document.ToList();
            var documents = (from document in dbResult
                           select new
                           {

                               document.Id_Type_Document,
                               document.Code_Type_Document,
                               document.Id_Type_Transaction
                           });

            return Json(documents, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateDocument(TR_Type_Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult Delete(int id = 0)
        {
            TR_Type_Document document = db.TR_Type_Document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            TR_Type_Document document = db.TR_Type_Document.Find(id);
            db.TR_Type_Document.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddDocument(TR_Type_Document document)
        {
            if (ModelState.IsValid)
            {
                db.TR_Type_Document.Add(document);
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