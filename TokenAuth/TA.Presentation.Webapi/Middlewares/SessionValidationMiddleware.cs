using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using TA.Domain.Interfaces;

public class SessionValidationMiddleware
{
    private readonly RequestDelegate _next;

    public SessionValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISessionRepository sessionRepository)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAuthorizeData>() != null)// if meta data contains auth data this if statement only works if you need auth ...
        {


            foreach (var claim in context.User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized: No valid token.");
                return;
            }


            //we have nameidentifier insted of sub yu must change it ....
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var sessionIdFromToken = context.User.FindFirst("sessionId").Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(sessionIdFromToken))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized: Missing token claims.");
                return;
            }

            int userId = int.Parse(userIdClaim);
            var currentSession = await sessionRepository.GetSessionByUserId(userId);

            if (currentSession == null || currentSession.SessionId != sessionIdFromToken)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized: Token session is invalid.");
                return;
            }
        }

        await _next(context);
    }
}