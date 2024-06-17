using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Infrastructure.Controllers;

[ApiController]
[Route("api/update")]
public class UpdateController(
    IUpdateHandler updateHandler,
    ITelegramBotClient botClient) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update, CancellationToken cancellationToken)
    {
        await updateHandler.HandleUpdateAsync(botClient, update, cancellationToken);
        return Ok();
    }
}