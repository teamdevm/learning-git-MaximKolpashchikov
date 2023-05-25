using System.Collections.Concurrent;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;

namespace CitizenshipSystem;

class BotServer
{
    private TelegramBotClient bot;
    private CancellationTokenSource cts;
    private ConcurrentDictionary<long,BotUI> client;

    public BotServer (string token)
    {
        bot = new TelegramBotClient(token);
        cts = new CancellationTokenSource();
        client = new ConcurrentDictionary<long,BotUI>();

        ReceiverOptions receiverOptions = new ReceiverOptions();
        receiverOptions.AllowedUpdates = new UpdateType[] { UpdateType.Message };

        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        bot.StartReceiving(
            updateHandler: HandleUpdate,
            pollingErrorHandler: HandlePollingError,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        Console.WriteLine("Becoming mindful.");
    }

    public void Stop ()
    {
        cts.Cancel();
    }

    private Task HandleUpdate (ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Only process Message updates
        if (update.Message is not Message message) return Task.CompletedTask;

        // Only process text messages
        if (message.Text is not string messageText) return Task.CompletedTask;

        long chatId = message.Chat.Id;
        messageText = messageText.ToLower().Trim();

        // DEBUG
        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

        if (messageText == "/start")
        {
            string hello = "";
            hello += "Привет!\n";
            hello += "Я знаю всё о Федеральном законе 'О гражданстве Российской Федерации'.\n";
            hello += "Могу сказать, имеешь ли ты право на вступление в гражданство РФ, если ответишь на несколько моих вопросов.\n";
            hello += "Если хочешь начать консультацию, напиши 'старт'.";
            bot.SendTextMessageAsync(chatId, hello);
        }
        else
        if (messageText == "старт")
        {
            if (client.ContainsKey(chatId))
            {
                bot.SendTextMessageAsync(chatId, "Консультация уже идёт.");
            }
            else
            {
                Task t = new Task(ConsultationThread, chatId, cancellationToken);
                t.Start();
            }
        }
        else
        {
            BotUI? ui;
            if (client.TryGetValue(chatId, out ui))
            {
                ui.msgQueue.Add(messageText);
            }
            else
            {
                bot.SendTextMessageAsync(chatId, "Если хочешь начать консультацию, напиши 'старт'.");
            }
        }

        return Task.CompletedTask;
    }

    private Task HandlePollingError (ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        string ErrorMessage;

        if (exception is ApiRequestException are)
        {
            ErrorMessage = $"Telegram API Error:\n[{are.ErrorCode}]\n{are.Message}";
        }
        else
        {
            ErrorMessage = exception.ToString();
        }

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

    private void ConsultationThread (object? obj)
    {
        if (obj is long chat)
        {
            BotUI io = new BotUI(bot, chat);
            ExpertCore ec = new ExpertCore(io);
            client.TryAdd(chat, io);
            ec.Consult();
            client.TryRemove(chat, out io!);
            io.msgQueue.Dispose();
        }
    }
}