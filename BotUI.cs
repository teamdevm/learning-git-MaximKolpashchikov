using System.Collections.Concurrent;
using Telegram.Bot;

namespace CitizenshipSystem;

class BotUI : ITalking
{
    private ITelegramBotClient gateway;
    public long chatId {get;}
    public BlockingCollection<string> msgQueue {get;}

    public BotUI (ITelegramBotClient bot, long chat)
    {
        chatId = chat;
        gateway = bot;
        msgQueue = new BlockingCollection<string>();
    }

    private string ReadLine ()
    {
        return msgQueue.Take();
    }

    private void WriteLine (string msg)
    {
        gateway.SendTextMessageAsync(chatId, msg);
    }

    public int Ask (string question)
    {
        int res = 0;
        bool err = true;
        string? rsp;

        WriteLine(question);

        do
        {
            rsp = ReadLine();
            if (string.IsNullOrWhiteSpace(rsp))
            {
                WriteLine("Строка была пустой");
                err = true;
            }
            else
            if (rsp.ToLower().Trim() == "да")
            {
                res = 2;
                err = false;
            }
            else
            if (rsp.ToLower().Trim() == "нет")
            {
                res = 1;
                err = false;
            }
            else
            {
                WriteLine("Да или нет. Третьего не дано");
                err = true;
            }
        }
        while (err);

        return res;
    }

    public int Ask (string question, int lower, int upper)
    {
        int res = 0;
        bool err = true;
        string? rsp;

        question += $"\nНапиши число от {lower} до {upper}.";
        WriteLine(question);

        do
        {
            rsp = ReadLine();
            if (string.IsNullOrWhiteSpace(rsp))
            {
                WriteLine("Строка была пустой");
                err = true;
            }
            else
            if (!int.TryParse(rsp, out res))
            {
                WriteLine("Это не число");
                err = true;
            }
            else
            if (res < lower || res > upper)
            {
                WriteLine("Число выходит за допустимые границы");
                err = true;
            }
            else
            err = false;
        }
        while (err);

        return res;
    }

    public int Ask (string question, IEnumerable<string> items)
    {
        int cnt = 0;


        question += "\n";
        foreach (string item in items)
        {
            question += $"\n{++cnt}: {item}";
        }
        question += "\n";

        return Ask(question, 1, items.Count());
    }

    public void Say (string message)
    {
        WriteLine(message);
    }
}