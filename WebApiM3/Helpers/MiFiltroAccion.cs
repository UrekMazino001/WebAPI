using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiM3.Helpers
{
    public class MiFiltroAccion : IActionFilter 
    {
        private readonly ILogger<MiFiltroAccion> logger;

        public MiFiltroAccion(ILogger<MiFiltroAccion> logger)
        {
            this.logger = logger;
        }
        public void    OnActionExecuting(ActionExecutingContext context)
        {
            //Se ejecuta antes de la accion
            logger.LogError("OnActionExecuting");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Se ejecuta despues
            logger.LogError("OnActionExecuted");
        }

    }
}
