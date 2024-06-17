using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Common.Interfaces.Telegram;

public interface ICommandHandler
{
    Task HandleCommandAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
}
