namespace CitizenshipSystem;

class StdUI : ITalking
{
    public int Ask (string question)
    {
        int res = 0;
        bool err = true;
        string? rsp;

        Console.WriteLine(question);

        do
        {
            rsp = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(rsp))
            {
                Console.WriteLine("Строка была пустой");
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
                Console.WriteLine("Да или нет. Третьего не дано");
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

        Console.WriteLine(question);
        Console.WriteLine($"Напиши число от {lower} до {upper}: ");

        do
        {
            rsp = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(rsp))
            {
                Console.WriteLine("Строка была пустой");
                err = true;
            }
            else
            if (!int.TryParse(rsp, out res))
            {
                Console.WriteLine("Это не число");
                err = true;
            }
            else
            if (res < lower || res > upper)
            {
                Console.WriteLine("Число выходит за допустимые границы");
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

        Console.WriteLine(question);
        Console.WriteLine("Выбери подходящий вариант:");

        foreach (string item in items)
        {
            Console.WriteLine($"{++cnt}: {item}");
        }

        return Ask("", 1, items.Count());
    }

    public void Say (string message)
    {
        Console.WriteLine(message);
    }
}