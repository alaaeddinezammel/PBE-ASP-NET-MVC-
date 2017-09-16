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
    public class ApprobationPBController : Controller
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
            where (c.Id_Type_Utilisateur == 4 || c.Id_Type_Utilisateur == 3) && c.Matricule == Matricule
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
            where (c.Id_Type_Utilisateur == 4 || c.Id_Type_Utilisateur == 3) && c.Matricule == Matricule
            select new
            {
                c.ACC_CODE
            };

            return Json(comptes.Distinct(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMois()
        {
            var dbResult = db.TR_Mois.ToList();
            var mois = (from m in dbResult
                        select new
                        {
                            m.Id_Mois,
                            m.Libelle_Mois
                        });
            return Json(mois, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetMouvements(string cmp_code, string Matricule, string ACC_CODE, int mois, int annee)
        {
            // int mois = 1; int annee = 2015; 
            //new ObjectParameter("@aCC_CODE", "0")
            //var dbResult = db.P_MAJ_PEP("0", "07002012    ", "0", 1, 2015, 1);
            var dbResult = db.P_MAJ_PEP(cmp_code, Matricule, ACC_CODE, mois, annee, 1);
            var mouvements = (from mvt in dbResult
                              select new
                              {

                                  mvt.MOVEMENT_ID,
                                  mvt.BANK_CODE,
                                  mvt.SITE,
                                  mvt.cmp,
                                  mvt.ACC_CODE,
                                  mvt.FLOW_CODE,
                                  mvt.BOOK_DATE,
                                  mvt.AMOUNT,
                                  mvt.SIGNE,
                                  mvt.DESCRIPTION,
                                  mvt.REFERENCE_NUMBER,
                                  mvt.MAN_Approbation,
                                  mvt.FS,
                                  mvt.PJ,
                                  mvt.MAN_Date_reception,
                                  mvt.MAN_Date_Approbation,
                                  mvt.Id_Revision,
                                  mvt.Id_Groupe_Utilisateur
                              });
            return Json(mouvements, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Approuver(int MovementID, bool b)
        {

            BK_MAN_LEDGER bk = (from mvt in db.BK_MAN_LEDGER

                                where mvt.MOVEMENT_ID == MovementID

                                select mvt).First();
            bk.MAN_Approbation = b;
            db.SaveChanges();
            return Json(bk.MAN_Reception, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Enregistrer(int MovementID, bool app, int id_rev, string Matricule, int id_group)
        {

            BK_MAN_LEDGER bk = (from mvt in db.BK_MAN_LEDGER

                                where mvt.MOVEMENT_ID == MovementID

                                select mvt).First();
            bk.MAN_Approbation = app;
            bk.Id_Groupe_Utilisateur = id_group;
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

            db.SaveChanges();
            return Json(bk.MAN_Reception, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Envoyer(string Matricule)
        {

            // BK_MAN_LEDGER mvt;
            if (ModelState.IsValid)
            {
                //db.Entry(mvt).State = EntityState.Modified;
                var dbResult = db.SP_WF(1, Matricule);
                // return Json(dbResult, JsonRequestBehavior.AllowGet);
                db.SaveChanges();
                return Json("true");
            }
            return Json("false");
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

        public JsonResult GetService()
        {
            var dbResult = db.TR_Groupe_Utilisateur.ToList();
            var services = (from service in dbResult
                            select new
                            {
                                service.Id_Groupe_Utilisateur,
                                service.Libelle_Groupe_Utilisateur
                            });

            return Json(services, JsonRequestBehavior.AllowGet);
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