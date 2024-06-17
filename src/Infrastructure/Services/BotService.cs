using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.Services;

public class BotService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUpdateHandler _updateHandler;

    public BotService(ITelegramBotClient botClient, IUpdateHandler updateHandler)
    {
        _botClient = botClient;
        _updateHandler = updateHandler;
    }

    public void Start()
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _botClient.StartReceiving(
            updateHandler: async (botClient, update, cancellationToken) => 
                await _updateHandler.HandleUpdateAsync(botClient, update, cancellationToken),
            pollingErrorHandler: async (botClient, exception, cancellationToken) => 
                await _updateHandler.HandlePollingErrorAsync(botClient, exception, cancellationToken),
            receiverOptions: receiverOptions
        );

        var me = _botClient.GetMeAsync().Result;
        Console.WriteLine($"Hello world! I am user {me.Id} and my name is {me.FirstName}");
    }
}