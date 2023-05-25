namespace CitizenshipSystem;

class Program
{
    public static void Main (string[] args)
    {
        StdUI ui = new StdUI();
        ExpertCore core = new ExpertCore(ui);
        core.Consult();
        // BotServer s = new BotServer("6233303920:AAEFCokbwS49Ejwny6_nhO1c2AvcCko4VHQ");
        // Console.ReadLine();
        // s.Stop();
    }
}
