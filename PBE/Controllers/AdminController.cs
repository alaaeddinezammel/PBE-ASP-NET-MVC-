using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PBE.Models;
using PBE.Data;

namespace PBE.Controllers
{
    public class AdminController : Controller
    {
        PBETSEntities1 bd = new PBETSEntities1();
        //
        // GET: /Admin/
        public ActionResult Index()
        {
        
            if (System.Web.HttpContext.Current.Session["Active"] != "not active")
            {

                System.Web.HttpContext.Current.Session["userwindows"] = User.Identity.Name.Substring(User.Identity.Name.Length - 8);
                string user = User.Identity.Name.Substring(User.Identity.Name.Length - 8);

                var ParamLogin = bd.Param_Login.FirstOrDefault(pr => pr.Matricule == user);

                if (ParamLogin != null)
                {
                    var ParamUser = bd.Param_User.FirstOrDefault(pr => pr.Matricule == user);
                    var Salary = bd.T_SALARIES.FirstOrDefault(pr => pr.Matricule == user);
                    System.Web.HttpContext.Current.Session["Matricule"] = ParamUser.Matricule;
                    System.Web.HttpContext.Current.Session["Identite"] = ParamUser.Identite;
                    System.Web.HttpContext.Current.Session["Poste"] = Salary.Libelle_Poste;
                    System.Web.HttpContext.Current.Session["Active"] = "estActive";
                    if (ParamUser.Est_Administrateur == true)
                    {
                        System.Web.HttpContext.Current.Session["user"] = "Admin";
                        return View();  
                    }
                    else
                    {
                        //var AgentSiege = bd.TR_USER_COMPTE.FirstOrDefault(pr => pr.Matricule == Login);
                        var ListCompteReception = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == user && pr.Id_Type_Utilisateur == 2);
                        var ListCompteApprobation = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == user && (pr.Id_Type_Utilisateur == 3 || pr.Id_Type_Utilisateur == 4));
                        var ListCompteConsultation = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == user && pr.Id_Type_Utilisateur == 1);
                        var ListAgentWFComptable = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == user);

                        if (ListCompteReception.Count() > 0 && ListCompteApprobation.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Reception et Approbation";
                            return View();
                        }
                        else if (ListCompteReception.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Reception";
                            return View();
                        }
                        else if (ListCompteApprobation.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Approbation";
                            return View();
                        }
                        else if (ListCompteConsultation.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Consultation";
                            //return RedirectToAction("MenuConsultation", "Admin");
                            return View();
                        }
                        else if (ListAgentWFComptable.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent WFComptable";
                            //return RedirectToAction("MenuWFComptable", "Admin");
                            return View();
                        } 
                        else
                        {
                            return RedirectToAction("_Login", "Account");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("_Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Connexion", "Account");
            }
        }
        // LOG OUT
        public ActionResult LogOut()
        {
            //return Redirect("Account/Login");
            System.Web.HttpContext.Current.Session["Active"] = "not active";
            System.Web.HttpContext.Current.Session["Matricule"] =null;
            System.Web.HttpContext.Current.Session["Identite"] = null;
            System.Web.HttpContext.Current.Session["Poste"] = null;
            return RedirectToAction("_Login", "Account");
        }
        //
        // GET: /Admin/Details/5
        public ActionResult MenuReception()
        {
            if (System.Web.HttpContext.Current.Session["UserActive"] == "not active")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                System.Web.HttpContext.Current.Session["otheruser"] = "otheruser";
                return View();
            }
        }

        public ActionResult MenuApprobation()
        {
            if (System.Web.HttpContext.Current.Session["UserActive"] == "not active")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                System.Web.HttpContext.Current.Session["otheruser"] = "otheruser";
                return View();
            }
        }

        public ActionResult MenuConsultation()
        {
            if (System.Web.HttpContext.Current.Session["UserActive"] == "not active")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                System.Web.HttpContext.Current.Session["otheruser"] = "otheruser";
                return View();
            }
        }
        public ActionResult MenuWFComptable()
        {
            if (System.Web.HttpContext.Current.Session["UserActive"] == "not active")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                System.Web.HttpContext.Current.Session["otheruser"] = "otheruser";
                return View();
            }
        }
	}
}