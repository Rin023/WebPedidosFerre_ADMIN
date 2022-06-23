using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;

namespace CapaPresentacionAdmin.Permisos
{
    public class PermisosRolAttribute : ActionFilterAttribute
    {

        private Rol idrol;

        public PermisosRolAttribute(Rol _idrol)
        {          
            idrol = _idrol;        
        }

      public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(HttpContext.Current.Session["ADMIN"]!= null)
            {
                Admin admin = HttpContext.Current.Session["ADMIN"] as Admin;

                if(admin.IdRol != this.idrol)
                {
                    filterContext.Result = new RedirectResult("~/Home/SinPermiso");
                  
                }

            }
            
            base.OnActionExecuting(filterContext);
        }


    }
}