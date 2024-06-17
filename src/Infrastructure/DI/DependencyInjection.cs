using Application.Common.Interfaces.Telegram;
using Application.TelegramUpdateHandlers.CommandHandlers;
using Infrastructure.Handlers;
using Infrastructure.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddBotServices(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var token = configuration["Telegram:Token"];

        if (token is null)
        {
            throw new Exception("Token was not found");
        }
        
        services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(token));
        services.AddSingleton<IUpdateHandler, UpdateHandler>();
        services.AddSingleton<BotService>();

        services.AddSingleton<ICommandHandler, StartCommandHandler>();
        services.AddSingleton<ICommandHandler, TestMarkupHandler>();

        return services;
    }
}