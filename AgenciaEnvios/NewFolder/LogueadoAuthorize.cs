using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AgenciaEnvios.WebApp.NewFolder
{
    public class LogueadoAuthorize:ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            int? logueadoId = context.HttpContext.Session.GetInt32("LogueadoId");
            if (logueadoId == null)
            {
                
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
