using UnityEngine;

public class ActionControllerBot : MonoBehaviour, IButtonParametrs
{

    private Player_Progress player_Systems;
    private Bot_Progress bot_Systems;
    public bool _changinEgconomy, _changingDevelopment;
    private float forceKick_bot, forceEcon_bot, forceDevelop_bot;

    public int costAttack_1 { set; get; }
    public int costAttack_2 { set; get; }
    public int costAttack_3 { set; get; }

    public int costEconomy_1 { set; get; }
    public int costEconomy_2 { set; get; }
    public int costEconomy_3 { set; get; }
    public int costDevelopment_1 { set; get; }
    public int costDevelopment_2 { set; get; }
    public int costDevelopment_3 { set; get; }


    private void Start()
    {
        player_Systems = FindObjectOfType<Player_Progress>();
        bot_Systems = FindObjectOfType<Bot_Progress>();
    }

    public void StartCharachterBot()
    {
        costAttack_1 = 7;
        costAttack_2 = 5;
        costAttack_3 = 6;

        costEconomy_1 = 5;
        costEconomy_2 = 4;
        costEconomy_3 = 3;

        costDevelopment_1 = 4;
        costDevelopment_2 = 7;
        costDevelopment_3 = 5;


        forceKick_bot = 0.07f;
        forceEcon_bot = 0.5f;
        forceDevelop_bot = 0.01f;
    }
    public void Attack_1()
    {
        if (bot_Systems.money >= costAttack_1)
        {
            player_Systems.StrikeOnTerritory = true;
            player_Systems.evolution -= forceKick_bot;
            player_Systems.progress -= forceKick_bot * 100;
            bot_Systems.money -= costAttack_1;
            costAttack_1 += 4;
        }
    }
    public void Attack_2()
    {
        if (bot_Systems.money >= costAttack_1)
        {
            _changinEgconomy = true;
            player_Systems.StrikeOnEconomick = true;
            player_Systems.MoneyPlus -= forceEcon_bot;
            bot_Systems.money -= costAttack_2;
            costAttack_2 += 5;
        }
    }
    public void Attack_3()
    {
        if (bot_Systems.money >= costAttack_3)
        {
            _changingDevelopment = false;
            player_Systems.StrikeOnDeveloper = true;
            player_Systems.SpeedEvolution -= forceDevelop_bot;
            bot_Systems.money -= costAttack_3;
            costAttack_3 += 4;
        }
    }
    public void Economy_1()
    {
        if (bot_Systems.money >= costEconomy_1)
        {
            _changinEgconomy = false;
            bot_Systems.StrikeOnEconomick = true;
            bot_Systems.MoneyPlus++;
            bot_Systems.SpeedEvolution -= 0.005f;
            bot_Systems.money -= costEconomy_1;
            costEconomy_1 += 3;

        }
    }
    public void Economy_2()
    {
        if (bot_Systems.money >= costEconomy_2)
        {
            _changinEgconomy = false;
            bot_Systems.StrikeOnEconomick = true;
            bot_Systems.MoneyPlus += 0.5f;
            bot_Systems.SpeedEvolution -= 0.002f;
            bot_Systems.money -= costEconomy_2;
            costEconomy_2 += 2;
        }
    }
    public void Economy_3()
    {
        if (bot_Systems.money >= costEconomy_3)
        {
            _changinEgconomy = false;
            bot_Systems.StrikeOnEconomick = true;
            bot_Systems.MoneyPlus += 0.4f;
            bot_Systems.SpeedEvolution -= 0.001f;
            bot_Systems.money -= costEconomy_3;
            costEconomy_3 += 1;
        }
    }
    public void Development_1()
    {
        if (bot_Systems.money >= costDevelopment_1)
        {
            _changingDevelopment = true;
            bot_Systems.StrikeOnDeveloper = true;
            bot_Systems.SpeedEvolution += 0.005f;
            bot_Systems.MoneyPlus -= 0.2f;
            bot_Systems.money -= costDevelopment_1;
            costDevelopment_1 += 3;
        }
    }
    public void Development_2()
    {
        if (bot_Systems.money >= costDevelopment_2)
        {
            _changingDevelopment = true;
            bot_Systems.StrikeOnDeveloper = true;
            bot_Systems.SpeedEvolution += 0.01f;
            bot_Systems.MoneyPlus -= 0.6f;
            bot_Systems.money -= costDevelopment_2;
            costDevelopment_2 += 4;
        }
    }
    public void Development_3()
    {
        if (bot_Systems.money >= costDevelopment_3)
        {
            _changingDevelopment = true;
            bot_Systems.StrikeOnDeveloper = true;
            bot_Systems.SpeedEvolution += 0.006f;
            bot_Systems.MoneyPlus -= 0.4f;
            forceKick_bot += 0.02f;
            forceEcon_bot += 0.5f;
            forceDevelop_bot += 0.002f;
            bot_Systems.money -= costDevelopment_3;
            costDevelopment_3 += 2;
        }
    }
    public void Bot_Analytics()
    {

        if (player_Systems.evolution >= 0.50f)
        {
            Attack_1();
            if (bot_Systems.money > 5)
                Rand();
        }
        else
        {
            Rand();
        }
    }

    private void Rand()
    {
        switch (RandomChoiceOfAction())
        {
            case 0:
                if (RandomChoiceOfSubAction() == 0 && player_Systems.evolution >= 0.10f)
                    Attack_1();
                else if (RandomChoiceOfSubAction() == 1 && player_Systems.MoneyPlus > 1)
                    Attack_2();
                else if (RandomChoiceOfSubAction() == 2 && player_Systems.SpeedEvolution > 0.01f)
                    Attack_3();
                else
                    RandomChoiceOfAction();
                break;
            case 1:
                if (RandomChoiceOfSubAction() == 0)
                    Economy_1();
                else if (RandomChoiceOfSubAction() == 1)
                    Economy_2();
                else if (RandomChoiceOfSubAction() == 2)
                    Economy_3();
                else
                    RandomChoiceOfAction();
                break;
            case 2:
                {
                    if (RandomChoiceOfSubAction() == 0)
                        Development_1();
                    else if (RandomChoiceOfSubAction() == 1)
                        Development_2();
                    else if (RandomChoiceOfSubAction() == 2)
                        Development_3();
                    else
                        RandomChoiceOfAction();
                }
                break;
        }

    }
    private int RandomChoiceOfAction() => Random.Range(0, 3);
    private int RandomChoiceOfSubAction() => Random.Range(0, 3);
}
