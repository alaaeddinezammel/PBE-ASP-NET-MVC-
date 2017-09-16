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
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.IO;

namespace PBE.Controllers
{
    public class ComptabilisationPBController : Controller
    {
        Data.PBETSEntities1 db = new Data.PBETSEntities1();

        // GET: GestionBanques
        public ActionResult Index(int MOVEMENT_ID)
        {
            ViewBag.MOVEMENT_ID = MOVEMENT_ID;
            return View();
        }

        public JsonResult GetHistorique(int MOVEMENT_ID)
        {
            var dbResult = db.T_Transaction.Where(t => t.MOVEMENT_ID == MOVEMENT_ID);
            var transactions = (from transaction in dbResult
                                select new
                                {
                                    transaction.Id_Action,
                                    transaction.MOVEMENT_ID,
                                    transaction.Matricule,
                                    transaction.Param_User.Identite,
                                    transaction.DateAction,
                                    transaction.Commentaire
                                });

            return Json(transactions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMouvement(int MOVEMENT_ID)
        {
            var dbResult = db.BK_MAN_LEDGER.Where(pr => pr.MOVEMENT_ID == MOVEMENT_ID);
            var mouvements = (from mvt in dbResult
                              select new
                              {
                                  mvt.ID_PJ,
                                  mvt.BANK_CODE,
                                  mvt.SITE,
                                  mvt.CMP_CODE,
                                  mvt.ACC_CODE,
                                  mvt.BOOK_DATE,
                                  mvt.SIGNE,
                                  mvt.AMOUNT,
                                  mvt.PJ,
                                  mvt.TRN_AMOUNT
                              });
            return Json(mouvements, JsonRequestBehavior.AllowGet);
        }

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

        public JsonResult GetActions()
        {
            var dbResult = db.TR_Action.ToList();
            var actions = (from action in dbResult
                           select new
                           {
                               action.Id_Action,
                               action.Libelle_Action
                           });

            return Json(actions, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RegroupperParFlux(int id_pj)
        {
            db.Comptabilisation_regroupper_parflux(id_pj);
            db.SaveChanges();
            return Json("true");
        }

        public JsonResult GetRevisions()
        {
            var dbResult = db.TR_Revision.ToList();
            var revisions = (from revision in dbResult
                             select new
                             {
                                 revision.Id_Revision,
                                 revision.Libelle_Revision
                             });

            return Json(revisions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRapprochement(int MOVEMENT_ID)
        {
            var dbResult = db.BK_MAN_REPORT.Where(pr => pr.MOVEMENT_ID == MOVEMENT_ID);
            var rapprochements = (from rapp in dbResult
                                  select new
                                  {
                                      rapp.BK_MAN_REPORT_ID,
                                      rapp.ACC_CODE,
                                      rapp.AMOUNT,
                                      rapp.TRN_AMOUNT,
                                      rapp.ORI_AMOUNT,
                                      rapp.FLOW_CODE,
                                      rapp.Id_Type_Document,
                                      rapp.CUR_CODE,
                                      rapp.REFERENCE_NUMBER,
                                      rapp.DESCRIPTION,
                                      rapp.BUDGET_CODE,
                                      rapp.Compte_comptable,
                                      rapp.SIGN,
                                      rapp.MPaiement,
                                      rapp.Projet,
                                      rapp.ORI_DESCRIPTION,
                                      rapp.ORI_REFERENCE_NUMBER
                                  });
            return Json(rapprochements, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMaxBK_MAN_REPORT_ID()
        {
            int Max = db.BK_MAN_REPORT.Max(r => r.BK_MAN_REPORT_ID);
            return Json(Max, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Enregistrer(int MovementID, bool app, int id_rev, string Matricule)
        {

            BK_MAN_LEDGER bk = (from mvt in db.BK_MAN_LEDGER

                                where mvt.MOVEMENT_ID == MovementID

                                select mvt).First();
            /*bk.MAN_Approbation = app;
            if (app == true)
            {

                bk.Validé = true;
                bk.Date_Approbation = DateTime.Now;//.ToString("yyyy-MM-ddThh:mm:sszzz");
                bk.MAN_Approbation = true;
                bk.MAN_Date_Approbation = DateTime.Now;
                bk.MAN_USER_App = Matricule;

            }
            else
            {

                bk.Validé = false;
                bk.Date_Approbation = null;//.ToString("yyyy-MM-ddThh:mm:sszzz");
                bk.MAN_Approbation = false;
                bk.MAN_Date_Approbation = null;
            }

            if (id_rev > 0)
            {
                bk.Id_Revision = id_rev;
                bk.MAN_Date_Approbation = null;
                bk.Validé = false;
                bk.Date_Approbation = null;
                bk.MAN_Approbation = false;
                bk.MAN_USER_App = Matricule;
            }

            db.SaveChanges();*/
            return Json(bk.MAN_Reception, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddRapprochement(BK_MAN_REPORT rapprochement, decimal mnt)
        {
            //var montant = (decimal)rapprochement.AMOUNT;
            //rapprochement.AMOUNT = montant;
            //float montant = rapprochement.AMOUNT;


            if (ModelState.IsValid)
            {
                rapprochement.AMOUNT = mnt;
                db.BK_MAN_REPORT.Add(rapprochement);
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        [HttpPost]
        public ActionResult Comptabiliser(string Matricule, int MovementID)
        {
            /* BK_MAN_LEDGER bk = (from mvt in db.BK_MAN_LEDGER

                                 where mvt.MOVEMENT_ID == MovementID

                                 select mvt).First();
             // BK_MAN_LEDGER mvt;
             int? id_pj = bk.ID_PJ;
             if (ModelState.IsValid)
             {
                 //db.Entry(mvt).State = EntityState.Modified;
                 var dbResult = db.SP_WFC(0, Matricule, id_pj, "");
                 // return Json(dbResult, JsonRequestBehavior.AllowGet);
                 db.SaveChanges();
                 return Json("true");
             }
             return Json("false");*/
            return View();
        }

        [HttpPost]
        public ActionResult Reviser(int id, string Matricule, int MovementID, string commentaire)
        {
            BK_MAN_LEDGER bk = (from mvt in db.BK_MAN_LEDGER

                                where mvt.MOVEMENT_ID == MovementID

                                select mvt).First();
            // BK_MAN_LEDGER mvt;
            int? id_pj = bk.ID_PJ;
            if (ModelState.IsValid)
            {
                //db.Entry(mvt).State = EntityState.Modified;
                var dbResult = db.SP_WFC(id, Matricule, id_pj, commentaire);
                // return Json(dbResult, JsonRequestBehavior.AllowGet);
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        [HttpPost]
        public ActionResult UpdateBK_MAN_LEDGER(BK_MAN_LEDGER BK, decimal nb)
        {
            if (ModelState.IsValid)
            {
                BK.AMOUNT = nb;
                db.Entry(BK).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("_Login", "Account");
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