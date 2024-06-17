using Application.Common.Interfaces.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.TelegramUpdateHandlers.CommandHandlers;

public class TestMarkupHandler : ICommandHandler
{
    public async Task HandleCommandAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        if (message.Text?.ToLower() != "/test")
        {
            return;
        }
        
        // клавиатура в самом низу
        // ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        // {
        //     new KeyboardButton[] { "Help me", "Call me ☎️" },
        // })
        // {
        //     ResizeKeyboard = true
        // };
        //
        // await botClient.SendTextMessageAsync(
        //     chatId: message.Chat.Id,
        //     text: "Choose a response",
        //     replyMarkup: replyKeyboardMarkup,
        //     cancellationToken: cancellationToken);
        
        InlineKeyboardMarkup inlineKeyboard = new(new[]
        {
            InlineKeyboardButton.WithSwitchInlineQuery(
                text: "switch_inline_query"),
            InlineKeyboardButton.WithSwitchInlineQueryCurrentChat(
                text: "switch_inline_query_current_chat"),
            InlineKeyboardButton.WithUrl("google", "https://www.google.com/"), 
            InlineKeyboardButton.WithWebApp("test game", new WebAppInfo {Url = "https://www.google.com/"})
        });

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "A message with an inline keyboard markup",
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);
    }
}