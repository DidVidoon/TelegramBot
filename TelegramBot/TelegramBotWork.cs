using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public class TelegramBotWork
    {
        private static string BotToken { get; set; } = "5005989032:AAHj4rpxPg3epzCE2ddxoN7Q-PS5T8qzULU";

        public async Task Run()
        {
            var botClient = new TelegramBotClient(BotToken);

            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

            var user = await botClient.GetMeAsync();

            Logs.Writer($"Start listening for @{user.Username}\n");
            Console.ReadLine();

            cts.Cancel();
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;
            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var user = update.Message.Chat.Username;
            var messageText = update.Message.Text;

            Logs.Writer($"Received a '{messageText}' message in chat {chatId}.");

            Logics logics = new(messageText.ToString().Split(' '), chatId, user, new GetRequestAPI());
            var answerText = logics.Run();

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: answerText,
                cancellationToken: cancellationToken);
        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
