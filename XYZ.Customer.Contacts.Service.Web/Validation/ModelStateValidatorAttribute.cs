using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace XYZ.Customer.Contacts.Service.Web.Validation
{
    /// <summary>
    /// The Attribute returns BadRequest if the model state is invalid
    /// </summary>
    public class ModelStateValidatorAttribute : ActionFilterAttribute
    {
        public ModelStateValidatorAttribute()
        {
            Order = 2;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
