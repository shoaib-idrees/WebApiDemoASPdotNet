using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo1.Models.Repositories;

namespace WebAPIDemo1.Filters.ExceptionFilters
{
    public class Shirt_HandleUpdateShirtFillterAttribute :ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var strShirt= context.RouteData.Values["id"] as string;
            if (int.TryParse(strShirt, out int id))
            {
                if (!ShirtRepository.ShirtExists(id)) 
                {
                    context.ModelState.AddModelError("ShirtId", "Shirt does not exists any more.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    { Status= StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);

                }
            }
        }
    }
}
