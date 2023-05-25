namespace CitizenshipSystem;

interface ITalking
{
    public void Say (string message);
    /* Asks a Yes/No question and returns the answer as 2/1 */
    public int Ask (string question);
    /* Asks for an integer and returns it */
    public int Ask (string question, int lower, int upper);
    /* Asks to choose an item from the list */
    public int Ask (string question, IEnumerable<string> items);
}

enum Fact
{
	CITIZEN,
	FBASIC,
	FLVLII,
	FLVLIII,
	FLIVE,
	FFAMILY,
	FCHILD,
	FWORK,
	FENTREP,
	FINVEST,
	OWNAGE,
	OLEGAL,
	LIVEAGE,
	INCOME,
	LANG,
	ACHIEVE,
	HIDEOUT,
	RUNAWAY,
	SPECIAL,
	CONTRACT,
	USSR,
	CIS,
	VETERAN,
	MIGRATE,
	OPARENT,
	MARIAGE,
	MCHILD,
	OCHILD,
	CHILDAGE,
	CLEGAL,
	CPARENT,
	WORKAGE,
	WORKTAX,
	WORKSPEC,
	EDUCATED,
	ENTREP,
	ENTTAX,
	INVEST,
	ISHARE,
	CAPITAL,
	INVTAX
}

class ExpertCore
{
    private ITalking client;
    private int[] memory;
    public ExpertCore (ITalking io)
    {
        client = io;
        memory = new int[Enum.GetValues<Fact>().Count()];
    }
    public void Consult ()
    {
        if (ThinkRoot() == 2)
        {
            client.Say("У тебя есть возможность получения гражданства РФ!");
        }
        else
        {
            client.Say("К сожалению, в выдаче гражданства тебе откажут.");
        }
    }
    private int Remember (Fact f)
    {
        return memory[(int)f];
    }
    private int Memorize (Fact f, int v)
    {
        memory[(int)f] = v;
        return v;
    }
    private int ThinkRoot ()
    {
        int r = Remember(Fact.CITIZEN);
        if (r == 0)
        {
            if (ThinkBasic() == 5) r = Memorize(Fact.CITIZEN, 2);
            else
            if (ThinkBasic() == 4)
                if (
                    ThinkContract() == 2
                    || ThinkLevel2() == 2
                    || ThinkLevel3() == 2
                ) r = Memorize(Fact.CITIZEN, 2);
                else
                r = Memorize(Fact.CITIZEN, 1);
            else
            if (ThinkBasic() == 3)
                if (
                    ThinkLevel2() == 2
                    || ThinkLevel3() == 2
                ) r = Memorize(Fact.CITIZEN, 2);
                else
                r = Memorize(Fact.CITIZEN, 1);
            else
            if (ThinkBasic() == 2)
                if (ThinkLevel3() == 2) r = Memorize(Fact.CITIZEN, 2);
                else
                r = Memorize(Fact.CITIZEN, 1);
            else
            r = Memorize(Fact.CITIZEN, 1);
        }
        return r;
    }
    private int ThinkBasic ()
    {
        int r = Remember(Fact.FBASIC);
        if (r == 0)
        {
            if (ThinkOwnAge() < 18) r = Memorize(Fact.FBASIC, 1);
            else
            if (ThinkOwnLegal() == 1) r = Memorize(Fact.FBASIC, 1);
            else
            if (ThinkLang() == 1) r = Memorize(Fact.FBASIC, 2);
            else
            if (ThinkIncome() == 1) r = Memorize(Fact.FBASIC, 3);
            else
            if (ThinkLive() == 1) r = Memorize(Fact.FBASIC, 4);
            else
            r = Memorize(Fact.FBASIC, 5);
        }
        return r;
    }
    private int ThinkLevel2 ()
    {
        int r = Remember(Fact.FLVLII);
        if (r == 0)
        {
            if (
                ThinkCIS() == 2
                || ThinkUSSR() == 2
                || ThinkFamily() == 2
                || ThinkChild() == 2
                || ThinkWork() == 2
                || ThinkEntrep() == 2
                || ThinkInvest() == 2
            ) r = Memorize(Fact.FLVLII, 2);
            else
            r = Memorize(Fact.FLVLII, 1);
        }
        return r;
    }

    private int ThinkLevel3 ()
    {
        int r = Remember(Fact.FLVLIII);
        if (r == 0)
        {
            if (
                ThinkMigrate() == 2
                || ThinkVeteran() == 2
                || ThinkSpecial() == 2
            ) r = Memorize(Fact.FLVLIII, 2);
            else
            r = Memorize(Fact.FLVLIII, 1);
        }
        return r;
    }

    private int ThinkLive ()
    {
        int r = Remember(Fact.FLIVE);
        if (r == 0)
        {
            if (ThinkLiveAge() == 1) r = Memorize(Fact.FLIVE, 1);
            else
            if (ThinkLiveAge() == 3) r = Memorize(Fact.FLIVE, 2);
            else
            if (
                ThinkRunaway() == 2
                || ThinkHideout() == 2
                || ThinkAchieve() == 2
            ) r = Memorize(Fact.FLIVE, 2);
            else
            r = Memorize(Fact.FLIVE, 1);
        }
        return r;
    }

    private int ThinkFamily ()
    {
        int r = Remember(Fact.FFAMILY);
        if (r == 0)
        {
            if (ThinkOwnParent() == 2) r = Memorize(Fact.FFAMILY, 2);
            else
            if (ThinkMariage() == 1) r = Memorize(Fact.FFAMILY, 1);
            else
            if (ThinkMariage() == 3) r = Memorize(Fact.FFAMILY, 2);
            else
            if (ThinkMariageChild() == 2) r = Memorize(Fact.FFAMILY, 2);
            else
            r = Memorize(Fact.FFAMILY, 1);
        }
        return r;
    }

    private int ThinkChild ()
    {
        int r = Remember(Fact.FCHILD);
        if (r == 0)
        {
            if (ThinkOwnChild() == 1) r = Memorize(Fact.FCHILD, 1);
            else
            if (
                ThinkChildAge() < 18
                || ThinkChildLegal() == 1
            )
                if (ThinkChildParent() == 1) r = Memorize(Fact.FCHILD, 1);
                else
                r = Memorize(Fact.FCHILD, 2);
            else
            r = Memorize(Fact.FCHILD, 2);
        }
        return r;
    }

    private int ThinkWork ()
    {
        int r = Remember(Fact.FWORK);
        if (r == 0)
        {
            if (ThinkWorkAge() == 1) r = Memorize(Fact.FWORK, 1);
            else
            if (ThinkWorkTax() == 1) r = Memorize(Fact.FWORK, 1);
            else
            if (ThinkEducation() == 2) r = Memorize(Fact.FWORK, 2);
            else
            if (ThinkWorkSpecial() == 2) r = Memorize(Fact.FWORK, 2);
            else
            r = Memorize(Fact.FWORK, 1);
        }
        return r;
    }

    private int ThinkEntrep ()
    {
        int r = Remember(Fact.FENTREP);
        if (r == 0)
        {
            if (ThinkEntrep2() < 3) r = Memorize(Fact.FENTREP, 1);
            else
            if (ThinkEntrepTax() == 1) r = Memorize(Fact.FENTREP, 1);
            else
            r = Memorize(Fact.FENTREP, 2);
        }
        return r;
    }

    private int ThinkInvest ()
    {
        int r = Remember(Fact.FINVEST);
        if (r == 0)
        {
            if (ThinkInvest2() == 1) r = Memorize(Fact.FINVEST, 1);
            else
            if (ThinkCapital() == 1) r = Memorize(Fact.FINVEST, 1);
            else
            if (ThinkInvestShare() < 10) r = Memorize(Fact.FINVEST, 1);
            else
            if (ThinkInvestTax() == 1) r = Memorize(Fact.FINVEST, 1);
            else
            r = Memorize(Fact.FINVEST, 2);
        }
        return r;
    }

    private int ThinkOwnAge ()
    {
        int r = Remember(Fact.OWNAGE);
        if (r == 0)
        {
            string msg = "Сколько тебе полных лет?";
            r = client.Ask(msg, 1, 100);
            Memorize(Fact.OWNAGE, r);
        }
        return r;
    }

    private int ThinkOwnLegal ()
    {
        int r = Remember(Fact.OLEGAL);
        if (r == 0)
        {
            string msg = "Ты дееспособен?";
            r = client.Ask(msg);
            Memorize(Fact.OLEGAL, r);
        }
        return r;
    }

    private int ThinkLiveAge ()
    {
        int r = Remember(Fact.LIVEAGE);
        if (r == 0)
        {
            string msg = "Сколько лет ты непрерывно проживаешь в РФ?";
            string[] ans = new string[]
            {
                "Меньше 1 или не проживаю вовсе",
                "От 1 до 5",
                "5 и более"
            };
            r = client.Ask(msg, ans);
            Memorize(Fact.LIVEAGE, r);
        }
        return r;
    }

    private int ThinkIncome ()
    {
        int r = Remember(Fact.INCOME);
        if (r == 0)
        {
            string msg = "У тебя есть законный источник средств к существованию?";
            r = client.Ask(msg);
            Memorize(Fact.INCOME, r);
        }
        return r;
    }

    private int ThinkLang ()
    {
        int r = Remember(Fact.LANG);
        if (r == 0)
        {
            string msg = "Ты владеешь русским языком (и это подтверждено документально)?";
            r = client.Ask(msg);
            Memorize(Fact.LANG, r);
        }
        return r;
    }

    private int ThinkAchieve ()
    {
        int r = Remember(Fact.ACHIEVE);
        if (r == 0)
        {
            string msg = "У тебя есть высокие достижения в области науки, техники и культуры?";
            r = client.Ask(msg);
            Memorize(Fact.ACHIEVE, r);
        }
        return r;
    }

    private int ThinkHideout ()
    {
        int r = Remember(Fact.HIDEOUT);
        if (r == 0)
        {
            string msg = "Тебе предоставлено политическое убежище на территории РФ?";
            r = client.Ask(msg);
            Memorize(Fact.HIDEOUT, r);
        }
        return r;
    }

    private int ThinkRunaway ()
    {
        int r = Remember(Fact.RUNAWAY);
        if (r == 0)
        {
            string msg = "У тебя есть статус беженца?";
            r = client.Ask(msg);
            Memorize(Fact.RUNAWAY, r);
        }
        return r;
    }

    private int ThinkSpecial ()
    {
        int r = Remember(Fact.SPECIAL);
        if (r == 0)
        {
            string msg = "У тебя есть особые заслуги перед РФ?";
            r = client.Ask(msg);
            Memorize(Fact.SPECIAL, r);
        }
        return r;
    }

    private int ThinkContract ()
    {
        int r = Remember(Fact.CONTRACT);
        if (r == 0)
        {
            string msg = "У тебя есть контракт о прохождении службы в Вооруженных Силах РФ на срок не менее одного года?";
            r = client.Ask(msg);
            Memorize(Fact.CONTRACT, r);
        }
        return r;
    }

    private int ThinkUSSR ()
    {
        int r = Remember(Fact.USSR);
        if (r == 0)
        {
            string msg = "У тебя было гражданство бывшего СССР?";
            r = client.Ask(msg);
            Memorize(Fact.USSR, r);
        }
        return r;
    }

    private int ThinkCIS ()
    {
        int r = Remember(Fact.CIS);
        if (r == 0)
        {
            string msg = "У тебя есть гражданство Республики Беларусь, Республики Казахстан, Республики Молдова или Украины?";
            r = client.Ask(msg);
            Memorize(Fact.CIS, r);
        }
        return r;
    }

    private int ThinkVeteran ()
    {
        int r = Remember(Fact.VETERAN);
        if (r == 0)
        {
            string msg = "Ты ветеран Великой Отечественной войны?";
            r = client.Ask(msg);
            Memorize(Fact.VETERAN, r);
        }
        return r;
    }

    private int ThinkMigrate ()
    {
        int r = Remember(Fact.MIGRATE);
        if (r == 0)
        {
            string msg = "Ты участник Государственной программы по оказанию содействия добровольному переселению в РФ соотечественников, проживающих за рубежом?";
            r = client.Ask(msg);
            Memorize(Fact.MIGRATE, r);
        }
        return r;
    }

    private int ThinkOwnParent ()
    {
        int r = Remember(Fact.OPARENT);
        if (r == 0)
        {
            string msg = "Среди твоих родителей есть граждане РФ, сейчас проживающие в РФ?";
            r = client.Ask(msg);
            Memorize(Fact.OPARENT, r);
        }
        return r;
    }

    private int ThinkMariage ()
    {
        int r = Remember(Fact.MARIAGE);
        if (r == 0)
        {
            string msg = "Ты состоишь в браке с гражданином РФ, сейчас проживающим в РФ? Если да, то как долго?";
            string[] ans = new string[]
            {
                "Нет",
                "Да, менее 3 лет",
                "Да, 3 года и более",
            };
            r = client.Ask(msg, ans);
            Memorize(Fact.MARIAGE, r);
        }
        return r;
    }

    private int ThinkMariageChild ()
    {
        int r = Remember(Fact.MCHILD);
        if (r == 0)
        {
            string msg = "У тебя есть общие дети в этом браке?";
            r = client.Ask(msg);
            Memorize(Fact.MCHILD, r);
        }
        return r;
    }

    private int ThinkOwnChild ()
    {
        int r = Remember(Fact.OCHILD);
        if (r == 0)
        {
            string msg = "У тебя есть сын/дочь, имеющий(-ая) гражданство РФ?";
            r = client.Ask(msg);
            Memorize(Fact.OCHILD, r);
        }
        return r;
    }

    private int ThinkChildAge ()
    {
        int r = Remember(Fact.CHILDAGE);
        if (r == 0)
        {
            string msg = "Сколько ему/ей полных лет?";
            r = client.Ask(msg, 1, 100);
            Memorize(Fact.CHILDAGE, r);
        }
        return r;
    }

    private int ThinkChildLegal ()
    {
        int r = Remember(Fact.CLEGAL);
        if (r == 0)
        {
            string msg = "Он/она признан(-а) дееспособным(-ой)?";
            r = client.Ask(msg);
            Memorize(Fact.CLEGAL, r);
        }
        return r;
    }

    private int ThinkChildParent ()
    {
        int r = Remember(Fact.CPARENT);
        if (r == 0)
        {
            string msg = "Другой его/её родитель умер/признан безвестно отсутствующим/недееспособным/лишен родительских прав?";
            r = client.Ask(msg);
            Memorize(Fact.CPARENT, r);
        }
        return r;
    }

    private int ThinkWorkAge ()
    {
        int r = Remember(Fact.WORKAGE);
        if (r == 0)
        {
            string msg = "Как долго ты работаешь в РФ?";
            string[] ans = new string[]
            {
                "Менее 1 года или не работаю вовсе",
                "1 год и более"
            };
            r = client.Ask(msg, ans);
            Memorize(Fact.WORKAGE, r);
        }
        return r;
    }

    private int ThinkWorkTax ()
    {
        int r = Remember(Fact.WORKTAX);
        if (r == 0)
        {
            string msg = "В указанный период работодатель начислял на тебя страховые взносы?";
            r = client.Ask(msg);
            Memorize(Fact.WORKTAX, r);
        }
        return r;
    }

    private int ThinkWorkSpecial ()
    {
        int r = Remember(Fact.WORKSPEC);
        if (r == 0)
        {
            string msg = "Твоя профессия включена в перечень профессий иностранных граждан и лиц без гражданства, имеющих право на приём в гражданство РФ в упрощённом порядке?";
            r = client.Ask(msg);
            Memorize(Fact.WORKSPEC, r);
        }
        return r;
    }

    private int ThinkEducation ()
    {
        int r = Remember(Fact.EDUCATED);
        if (r == 0)
        {
            string msg = "У тебя есть профессиональное образование, полученное на территории РФ?";
            r = client.Ask(msg);
            Memorize(Fact.EDUCATED, r);
        }
        return r;
    }

    private int ThinkEntrep2 ()
    {
        int r = Remember(Fact.ENTREP);
        if (r == 0)
        {
            string msg = "Ты индивидуальный предприниматель? Если да, то как долго ты ведёшь предпринимательскую деятельность в РФ?";
            string[] ans = new string[]
            {
                "Нет",
                "Да, менее 3 лет",
                "Да, 3 года и более"
            };
            r = client.Ask(msg, ans);
            Memorize(Fact.ENTREP, r);
        }
        return r;
    }

    private int ThinkEntrepTax ()
    {
        int r = Remember(Fact.ENTTAX);
        if (r == 0)
        {
            string msg = "В указанный период сумма уплаченных твоим ИП в каждом календарном году налогов и страховых взносов составляет не менее 1 млн руб?";
            r = client.Ask(msg);
            Memorize(Fact.ENTTAX, r);
        }
        return r;
    }

    private int ThinkInvest2 ()
    {
        int r = Remember(Fact.INVEST);
        if (r == 0)
        {
            string msg = "Ты инвестор, имеющий долю в уставном капитале российского юридического лица?";
            r = client.Ask(msg);
            Memorize(Fact.INVEST, r);
        }
        return r;
    }

    private int ThinkInvestShare ()
    {
        int r = Remember(Fact.ISHARE);
        if (r == 0)
        {
            string msg = "Какова твоя доля вклада (в процентах, округли до целого)?";
            r = client.Ask(msg, 1, 100);
            Memorize(Fact.ISHARE, r);
        }
        return r;
    }

    private int ThinkCapital ()
    {
        int r = Remember(Fact.CAPITAL);
        if (r == 0)
        {
            string msg = "Размер уставного капитала указанного юридического лица превышает 100 млн руб?";
            r = client.Ask(msg);
            Memorize(Fact.CAPITAL, r);
        }
        return r;
    }

    private int ThinkInvestTax ()
    {
        int r = Remember(Fact.INVTAX);
        if (r == 0)
        {
            string msg = "В указанный период сумма уплаченных указанным юридическим лицом в каждом календарном году налогов и страховых взносов составляет не менее 6 млн руб?";
            r = client.Ask(msg);
            Memorize(Fact.INVTAX, r);
        }
        return r;
    }
}