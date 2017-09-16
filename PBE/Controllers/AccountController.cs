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

namespace PBE.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        Data.PBETSEntities1 bd = new Data.PBETSEntities1();

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {

            //ViewBag.ReturnUrl = returnUrl;
            //AgentSiege loggedUser = PbDB.AgentSieges.FirstOrDefault(acc => acc.Matricule == id);
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(FormCollection user)
        {
            try
            {
                string Login = user["UserName"];
                string Password = user["Password"];

                var ParamLogin = bd.Param_Login.FirstOrDefault(pr => pr.Matricule == Login);

                if (ParamLogin != null && Password == ParamLogin.Mpt_User)
                {
                    var ParamUser = bd.Param_User.FirstOrDefault(pr => pr.Matricule == Login);
                    var Salary = bd.T_SALARIES.FirstOrDefault(pr => pr.Matricule == Login);
                    System.Web.HttpContext.Current.Session["Matricule"] = ParamUser.Matricule;
                    System.Web.HttpContext.Current.Session["Identite"] = ParamUser.Identite;
                    System.Web.HttpContext.Current.Session["Poste"] = Salary.Libelle_Poste;
                    if (ParamUser.Est_Administrateur == true)
                    {
                        System.Web.HttpContext.Current.Session["user"] = "Admin";
                        //System.Web.HttpContext.Current.Session["Active"] = "active";
                        return RedirectToAction("Connexion", "Account");
                    }
                    else
                    {
                        //var AgentSiege = bd.TR_USER_COMPTE.FirstOrDefault(pr => pr.Matricule == Login);
                        var ListCompteReception = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == Login && pr.Id_Type_Utilisateur == 2);
                        var ListCompteApprobation = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == Login && (pr.Id_Type_Utilisateur == 3 || pr.Id_Type_Utilisateur == 4));
                        var ListCompteConsultation = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == Login && pr.Id_Type_Utilisateur == 1);
                        var ListAgentWFComptable = bd.TJ_USER_GROUPE.Where(pr => pr.Matricule == Login);
                        if (ListCompteReception.Count() > 0 && ListCompteApprobation.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Reception et Approbation";
                            return RedirectToAction("Connexion", "Account");
                        }

                        else if (ListCompteReception.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Reception";
                            return RedirectToAction("Connexion", "Account");
                        }
                        else if (ListCompteApprobation.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Approbation";
                            return RedirectToAction("Connexion", "Account");
                        }
                        else if (ListCompteConsultation.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Consultation";
                            return RedirectToAction("Connexion", "Account");
                        }
                        else if (ListAgentWFComptable.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent WFComptable";
                            return RedirectToAction("Connexion", "Account");
                        }
                        else
                        {
                            return RedirectToAction("_Login", "Account");
                        }
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View();
                }

            }
            catch
            {
                //return RedirectToAction("Login");
                return View();
            }
        }

        public ActionResult _Login(FormCollection user)
        {
            try
            {
                string Login = user["UserName"];
                string Password = user["Password"];

                var ParamLogin = bd.Param_Login.FirstOrDefault(pr => pr.Matricule == Login);

                if (ParamLogin != null && Password == ParamLogin.Mpt_User)
                {
                    var ParamUser = bd.Param_User.FirstOrDefault(pr => pr.Matricule == Login);
                    var Salary = bd.T_SALARIES.FirstOrDefault(pr => pr.Matricule == Login);
                    System.Web.HttpContext.Current.Session["Identite"] = ParamUser.Identite;
                    System.Web.HttpContext.Current.Session["Matricule"] = ParamUser.Matricule;
                    System.Web.HttpContext.Current.Session["Poste"] = Salary.Libelle_Poste;
                    System.Web.HttpContext.Current.Session["UserActive"] = "active";

                    if (ParamUser.Est_Administrateur == true)
                    {
                        System.Web.HttpContext.Current.Session["user"] = "Admin";
                        //System.Web.HttpContext.Current.Session["Active"] = "active";
                        return RedirectToAction("Connexion", "Account");
                    }
                    else 
                    {
                        //var AgentSiege = bd.TR_USER_COMPTE.FirstOrDefault(pr => pr.Matricule == Login);
                        var ListCompteReception = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == Login && pr.Id_Type_Utilisateur == 2);
                        var ListCompteApprobation = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == Login && (pr.Id_Type_Utilisateur == 3 || pr.Id_Type_Utilisateur == 4));
                        var ListCompteConsultation = bd.TR_USER_COMPTE.Where(pr => pr.Matricule == Login && pr.Id_Type_Utilisateur == 1);
                        var ListAgentWFComptable = bd.TJ_USER_GROUPE.Where(pr => pr.Matricule == Login);
                        if (ListCompteReception.Count() > 0 && ListCompteApprobation.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Reception et Approbation";
                            return RedirectToAction("Connexion", "Account");
                        }
                        else if(ListCompteReception.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Reception";
                            return RedirectToAction("Connexion", "Account");
                        }
                        else if (ListCompteApprobation.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Approbation";
                            return RedirectToAction("Connexion", "Account");
                        }
                        else if (ListCompteConsultation.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent Consultation";
                            return RedirectToAction("Connexion", "Account");
                        }
                        else if (ListAgentWFComptable.Count() > 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = "Agent WFComptable";
                            return RedirectToAction("Connexion", "Account");
                        } 
                        else
                        {
                            return RedirectToAction("_Login", "Account");
                        }
                    }
                    
                }

                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View();
                }

            }
            catch
            {
                //return RedirectToAction("Login");
                return View();
            }
        }
        public ActionResult Connexion()
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

        public ActionResult LogOut()
        {
            System.Web.HttpContext.Current.Session["Active"] = "active";
            System.Web.HttpContext.Current.Session["UserActive"] = "not active";
            System.Web.HttpContext.Current.Session["Matricule"] = null;
            System.Web.HttpContext.Current.Session["Identite"] = null;
            System.Web.HttpContext.Current.Session["Poste"] = null;
            return RedirectToAction("_Login", "Account");
        }
    }
}