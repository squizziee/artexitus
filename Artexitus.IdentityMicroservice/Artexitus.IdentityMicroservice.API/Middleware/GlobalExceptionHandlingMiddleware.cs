using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.IdentityMicroservice.API.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var status = StatusCodes.Status418ImATeapot;

                switch (exception)
                {
                    case MissingValidatorException:
                        status = StatusCodes.Status500InternalServerError;
                        break;

                    case ResourceDoesNotExistException:
                        status = StatusCodes.Status404NotFound;
                        break;

                    case UnacceptableCommandException:
                        status = StatusCodes.Status405MethodNotAllowed;
                        break;

                    case ValidationException:
                        status = StatusCodes.Status400BadRequest;
                        break;

                    case DbUpdateException:
                        status = StatusCodes.Status409Conflict;
                        break;
                }

                var problemDetails = new ProblemDetails
                {
                    Status = status,
                    Title = exception.Message,
                };

                context.Response.StatusCode = status;
                await context.Response
                    .WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
