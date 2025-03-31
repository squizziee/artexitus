using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.IdentityMicroservice.API.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, 
            ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
                var message = exception.Message;

                switch (exception)
                {
                    case MissingValidatorException:
                        status = StatusCodes.Status500InternalServerError;
                        break;

                    case ResourceDoesNotExistException:
                        status = StatusCodes.Status404NotFound;
                        break;

                    case AlreadyExistsException:
                        status = StatusCodes.Status409Conflict;
                        break;

                    case InvalidCredentialsException:
                        status = StatusCodes.Status401Unauthorized;
                        break;

                    case UnacceptableCommandException:
                        status = StatusCodes.Status405MethodNotAllowed;
                        break;

                    case ValidationException:
                        status = StatusCodes.Status400BadRequest;
                        break;

                    case DbUpdateException:
                        status = StatusCodes.Status409Conflict;
                        message = "Invalid request";
                        break;
                }

                _logger.LogError("{ex}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = status,
                    Title = message,
                };

                context.Response.StatusCode = status;
                await context.Response
                    .WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
