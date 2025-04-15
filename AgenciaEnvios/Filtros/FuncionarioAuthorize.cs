using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AgenciaEnvios.WebApp.NewFolder
{
    public class FuncionarioAuthorize: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRole = context.HttpContext.Session.GetString("LogueadoRol");
            if (userRole != "Funcionario")
            {
                
                context.Result = new RedirectToActionResult("AccesoDenegado", "Usuario", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
