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
    public class GestionTransactionController : Controller
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
                                 transaction.Code_Type_Transaction,
                                 transaction.Libelle_Type_Transaction
                             });

            return Json(transactions, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateTransaction(TR_Type_Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult Delete(int id = 0)
        {
            TR_Type_Transaction transaction = db.TR_Type_Transaction.Find(id);
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
            TR_Type_Transaction transaction = db.TR_Type_Transaction.Find(id);
            db.TR_Type_Transaction.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddTransaction(TR_Type_Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.TR_Type_Transaction.Add(transaction);
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