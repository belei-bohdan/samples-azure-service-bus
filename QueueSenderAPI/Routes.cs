using QueueSenderAPI.Services;
using Shared.Models;

namespace QueueSenderAPI
{
    public static class Routes
    {
        public static void MapRoutes(this IEndpointRouteBuilder app)
        {
            app.MapPost("/messages", async (ISenderService senderService, string author, string message) =>
            {
                await senderService.SendMessageAsync(new Message(message, author));
                return Results.Accepted();
            })
            .WithName("Create")
            .WithOpenApi();
        }
    }
}
