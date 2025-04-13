using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgenciaEnvios.WebApp.NewFolder
{
    public class ClienteAuthorize: ActionFilterAttribute
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
