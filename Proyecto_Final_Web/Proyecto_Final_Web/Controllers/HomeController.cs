using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Final_Web.Models;
namespace Proyecto_Final_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            
            FinalWebEntities fn = new FinalWebEntities();
            List<AspNetUser> lista = fn.AspNetUsers.ToList();

            ViewData["Usuarios"] = from u in lista select new Listas { listaUsuarios = u };

            return View(ViewData["Usuarios"]);
        }

   
        public ActionResult Afiliados(int idSocio)
        {
            FinalWebEntities sd = new FinalWebEntities();
          
            List<Afiliado> afiliadoslista = sd.Afiliados.ToList();
           

            ViewData["Afiliados"] = from a in afiliadoslista
                                    where a.idSocio == idSocio
                                    select new Listas { listaAfiliados = a};

          

            return View(ViewData["Afiliados"]);

         
        }

        public ActionResult guardarAfiliados( int idSocio)
        {
            FinalWebEntities db = new FinalWebEntities();
         
            Afiliado AFL = new Afiliado();
            AFL.Nombre = Request.Form["Nombre"];
            AFL.Apellidos = Request.Form["Apellidos"];
            AFL.telefono = Request.Form["telefono"];
            AFL.Parentezco = Request.Form["Parentezco"]; 
            AFL.idSocio = idSocio;

            if (!String.IsNullOrEmpty(AFL.telefono))
            {
                db.Afiliados.Add(AFL);
                db.SaveChanges();
                return RedirectToAction("Index", "Socios");
            }


            return View();

            
        }
    }
}