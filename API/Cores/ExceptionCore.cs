using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

using System.Security;
using System.Security.Authentication;
using System.Text;

namespace API.Cores
{
    public static class ExceptionCore
    {
    }

    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        //This Order allows other filters to run at the end of the pipeline.
        public int Order => int.MaxValue - 10;

        private static string GetCurrentUrl(ActionContext context) => context.HttpContext.Request.GetDisplayUrl();

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errorMessages = new StringBuilder();

            var errors = context
                .ModelState
                .Values
                .Where(p => p.Errors.Any())
                .Select(p => p.Errors)
                .AsParallel();

            Parallel.ForEach(errors, item =>
            {
                for (int i = 0; i < item.Count; i++)
                {
                    errorMessages.Append(' ');
                    errorMessages.Append(item[i].ErrorMessage);
                }
            });

            var problem = new ProblemModel(errorMessages.ToString(), GetCurrentUrl(context), 400, "Validation Error");

            context.Result = new ObjectResult(problem)
            {
                StatusCode = problem.Status
            };
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Exception ex)
            {
                var url = GetCurrentUrl(context);
                var status = ex switch
                {
                    InvalidOperationException => 403,
                    KeyNotFoundException or NullReferenceException => 404,
                    DbUpdateException => 400,
                    VerificationException or AuthenticationException or SecurityException => 401,
                    TimeoutException => 408,
                    _ => 500
                };
                var problem = new ProblemModel(ex.Message, url, status);

                context.Result = new ObjectResult(problem)
                {
                    StatusCode = problem.Status
                };

                context.ExceptionHandled = true;
            }
        }
    }
}