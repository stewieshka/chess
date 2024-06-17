using Application.Common.Interfaces.Telegram;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.Handlers;

public class UpdateHandler : IUpdateHandler
{
    private readonly IEnumerable<ICommandHandler> _commandHandlers;

    public UpdateHandler(IEnumerable<ICommandHandler> commandHandlers)
    {
        _commandHandlers = commandHandlers;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update is { Type: UpdateType.Message, Message.Type: MessageType.Text })
        {
            var message = update.Message;
            foreach (var handler in _commandHandlers)
            {
                await handler.HandleCommandAsync(botClient, message, cancellationToken);
            }
        }
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Error: {apiRequestException.ErrorCode}\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);

        return Task.CompletedTask;
    }
}