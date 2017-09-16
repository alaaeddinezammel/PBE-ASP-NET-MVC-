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
    public class ReceptionPBController : Controller
    {
        Data.PBETSEntities1 db = new Data.PBETSEntities1();

        // GET: GestionBanques
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCompanies(string Matricule)
        {

           var societes =
           from a in db.COMPANIES
           join cmp in db.ACCOUNTS on a.CMP_CODE equals cmp.CMP_CODE
           join c in db.TR_USER_COMPTE on cmp.ACC_CODE equals c.ACC_CODE
           where c.Id_Type_Utilisateur == 2 && c.Matricule == Matricule
           select new
           {
               a.CMP_CODE,
               a.DESCRIPTION
           };

           return Json(societes.Distinct(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccounts(string Matricule)
        {

            var comptes =

            from c in db.TR_USER_COMPTE 
            where c.Id_Type_Utilisateur == 2 && c.Matricule == Matricule
            select new
            {
                c.ACC_CODE
            };

            return Json(comptes.Distinct(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMois()
        {
            var dbResult =db.TR_Mois.ToList();
            var mois = (from m in dbResult
                        select new
                        {
                            m.Id_Mois,
                            m.Libelle_Mois
                        });
            return Json(mois, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetMouvements(string cmp_code, string Matricule, string ACC_CODE, int mois, int annee)
        {
           // int mois = 1; int annee = 2015; 
            DateTime du = new DateTime(2013, 01, 01);
            DateTime au = new DateTime(2015, 12, 31);
            //new ObjectParameter("@aCC_CODE", "0")
            //var dbResult = db.P_MAJ_PENP(du, au, "0", "07002012    ", "0", 1, 2015, 1);
             var dbResult = db.P_MAJ_PENP(du, au, cmp_code, Matricule, ACC_CODE, mois, annee, 1);
            var mouvements = (from mvt in dbResult
                           select new
                           {

                               mvt.MOVEMENT_ID,
                               mvt.BANK_CODE,
                               mvt.SITE,
                               mvt.cmp,
                               mvt.CMP_CODE,
                               mvt.ACC_CODE,
                               mvt.FLOW_CODE,
                               mvt.BOOK_DATE,
                               mvt.VALUE_DATE, 
                               mvt.AMOUNT,
                               mvt.SIGNE,
                               mvt.IBC_ZONE_1,
                               mvt.IBC_ZONE_2, 
                               mvt.ELECTRONIC_VALUE,
                               mvt.Ref,
                               mvt.Validé,
                               mvt.DESCRIPTION,
                               mvt.REFERENCE_NUMBER,
                               mvt.MAN_Reception,
                               mvt.FS,
                               mvt.PJ,
                               mvt.MAN_Date_reception,
                               mvt.MAN_Ref_Bord,
                               mvt.Id_Revision,
                               mvt.E
                           });
            return Json(mouvements, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateBK_MAN_LEDGER(BK_MAN_LEDGER BK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(BK).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }


        [HttpPost]
        public ActionResult Envoyer(string Matricule)
        {

           // BK_MAN_LEDGER mvt;
            if (ModelState.IsValid)
            {
                //db.Entry(mvt).State = EntityState.Modified;
                var dbResult = db.SP_WF(0, Matricule);
               // return Json(dbResult, JsonRequestBehavior.AllowGet);
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
        }

        [HttpPost]
        public JsonResult RattacherPJ(string fileName, int MovementID,string Matricule)
        {

                    BK_MAN_LEDGER bk = (from mvt in db.BK_MAN_LEDGER

                             where mvt.MOVEMENT_ID == MovementID

                             select mvt).First();
                    bk.PJ = fileName;
                    //bk.MAN_Reception = true;
                    bk.MAN_USER_Recp = Matricule;
                    db.SaveChanges();
                    return Json(fileName, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Verifier(int MovementID, bool b)
        {

            BK_MAN_LEDGER bk = (from mvt in db.BK_MAN_LEDGER

                                where mvt.MOVEMENT_ID == MovementID

                                select mvt).First();
            bk.MAN_Reception = b;
            db.SaveChanges();
            return Json(bk.MAN_Reception, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePJ(int MovementID)
        {

            BK_MAN_LEDGER bk = (from mvt in db.BK_MAN_LEDGER

                                where mvt.MOVEMENT_ID == MovementID

                                select mvt).First();
            bk.PJ = null;
            db.SaveChanges();
            return Json(bk.PJ, JsonRequestBehavior.AllowGet);
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