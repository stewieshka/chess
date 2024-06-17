using Application.Common.Interfaces.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.TelegramUpdateHandlers.CommandHandlers;

public class StartCommandHandler : ICommandHandler
{
    public async Task HandleCommandAsync(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken)
    {
        if (message.Text?.ToLower() != "/start")
        {
            return;
        }

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Welcome to the bot!",
            cancellationToken: cancellationToken);
    }
}