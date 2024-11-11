using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmartE_commercePlatform.Helpers
{
    public class FilterExceptions : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> exceptionDictionary;

        public FilterExceptions()
        {
            exceptionDictionary = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ResourceNotFoundException), HandleResourceNotFoundException },
                { typeof(ConflictException), HandleConflictException },
                { typeof(CustomException), HandleCustomException },
                { typeof(ForbiddenException), HandleForbiddenException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type exceptionType = context.Exception.GetType();

            if (exceptionDictionary.ContainsKey(exceptionType))
            {
                exceptionDictionary[exceptionType].Invoke(context);
                return;
            }
        }

        private void HandleCustomException(ExceptionContext context)
        {
            CustomException exception = (CustomException)context.Exception;
            
            context.Result = new BadRequestObjectResult(exception.ExceptionMessages);

            context.ExceptionHandled = true;
        }

        private void HandleResourceNotFoundException(ExceptionContext context)
        {
            var exception = (ResourceNotFoundException)context.Exception;

            context.Result = new NotFoundObjectResult(exception.Message);

            context.ExceptionHandled = true;
        }

        private void HandleConflictException(ExceptionContext context)
        {
            var exception = (ConflictException)context.Exception;

            context.Result = new ConflictObjectResult(exception.Message);

            context.ExceptionHandled = true;
        }

        private void HandleForbiddenException(ExceptionContext context)
        {
            var exception = (ForbiddenException)context.Exception;

            context.Result = new ForbidResult(exception.Message);

            context.ExceptionHandled = true;
        }
    }
}
