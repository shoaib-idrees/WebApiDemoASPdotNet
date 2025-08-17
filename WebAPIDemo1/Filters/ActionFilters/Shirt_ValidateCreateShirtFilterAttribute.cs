using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo1.Models;
using WebAPIDemo1.Models.Repositories;

namespace WebAPIDemo1.Filters.ActionFilters
{
    public class Shirt_ValidateCreateShirtFilterAttribute :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var shirt = context.ActionArguments["shirt"] as Shirt;

            if (shirt == null)
            {
                context.ModelState.AddModelError("shirt", "Shirt Object is null");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                var existingShirt = ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);
                if (existingShirt != null)
                {
                    context.ModelState.AddModelError("Shirt", "Shirt alraedy exists.");
                    var problemdetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result= new BadRequestObjectResult(problemdetails);
                }
            }
        }
    }
}
