using UnityEngine;
using UnityEngine.UIElements;

public class VariableActionPlayer : MonoBehaviour, IButtonParametrs
{
    private Player_Progress _player_Progress;
    private Bot_Progress _bot_Progress;
    private ActionControllerPlayer _actionControllerPlayer;
    private float _forceKick, _forceEcon, _forceDevelop;
    private bool _changinEgconomy, _сhangingDevelopment;
    private int _upgradeIconAbilitiesInt;
    public int UpgradeIconAbilities { get => _upgradeIconAbilitiesInt;set => _upgradeIconAbilitiesInt = value; }
    public int costAttack_1 { set; get; }
    public int costAttack_2 { set; get; }
    public int costAttack_3 { set; get; }
    public int costEconomy_1 { set; get; }
    public int costEconomy_2 { set; get; }
    public int costEconomy_3 { set; get; }
    public int costDevelopment_1 { set; get; }
    public int costDevelopment_2 { set; get; }
    public int costDevelopment_3 { set; get; }
    public bool ChanginEgconomy { get => _changinEgconomy; set => _changinEgconomy = value; }
    public bool ChangingDevelopment { get => _сhangingDevelopment; set => _сhangingDevelopment = value; }
    private void Start()
    {
        _player_Progress = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Progress>();
        _bot_Progress = GameObject.FindGameObjectWithTag("Bot").GetComponent<Bot_Progress>();
        _actionControllerPlayer = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<ActionControllerPlayer>();
    }

    public void StartCharachters()
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

        _forceKick = 0.07f;
        _forceEcon = 0.5f;
        _forceDevelop = 0.01f;
    }

    public void Attack_1() //Territoria
    {
        if (_player_Progress.money >= costAttack_1)
        {
            _bot_Progress.StrikeOnTerritory = true;
            _bot_Progress.evolution -= _forceKick;
            _bot_Progress.progress -= _forceKick * 100;
            _player_Progress.money -= costAttack_1;
            costAttack_1 += 4;
        }
    }
    public void Attack_2() //Economick
    {
        if (_player_Progress.money >= costAttack_2)
        {
            ChanginEgconomy = true;
            _bot_Progress.StrikeOnEconomick = true;
            _bot_Progress.MoneyPlus -= _forceEcon;
            _player_Progress.money -= costAttack_2;
            costAttack_2 += 5;
        }
    }
    public void Attack_3() //Developer
    {
        if (_player_Progress.money >= costAttack_3)
        {
            ChangingDevelopment = false;
            _bot_Progress.StrikeOnDeveloper = true;
            _bot_Progress.SpeedEvolution -= _forceDevelop;
            _player_Progress.money -= costAttack_3;
            costAttack_3 += 4;
        }
    }

    public void Economy_1() //Tax
    {
        if (_player_Progress.money >= costEconomy_1)
        {
            ChanginEgconomy = false;
            _player_Progress.StrikeOnEconomick = true;
            _player_Progress.MoneyPlus++;
            _player_Progress.SpeedEvolution -= 0.005f;
            _player_Progress.money -= costEconomy_1;
            costEconomy_1 += 3;
        }
    }
    public void Economy_2() //Population
    {
        if (_player_Progress.money >= costEconomy_2)
        {
            ChanginEgconomy = false;
            _player_Progress.StrikeOnEconomick = true;
            _player_Progress.MoneyPlus += 0.5f;
            _player_Progress.SpeedEvolution -= 0.002f;
            _player_Progress.money -= costEconomy_2;
            costEconomy_2 += 2;
        }
    }
    public void Economy_3() //Expectancy
    {
        if (_player_Progress.money >= costEconomy_3)
        {
            ChanginEgconomy = false;
            _player_Progress.StrikeOnEconomick = true;
            _player_Progress.MoneyPlus += 0.4f;
            _player_Progress.SpeedEvolution -= 0.001f;
            _player_Progress.money -= costEconomy_3;
            costEconomy_3 += 1;
        }
    }
    public void Development_1() //Medecin
    {
        if (_player_Progress.money >= costDevelopment_1)
        {
            ChangingDevelopment = true;
            _player_Progress.StrikeOnDeveloper = true;
            _player_Progress.SpeedEvolution += 0.005f;
            _player_Progress.MoneyPlus -= 0.2f;
            _player_Progress.money -= costDevelopment_1;
            costDevelopment_1 += 3;
        }
    }
    public void Development_2() //Commercia
    {
        if (_player_Progress.money >= costDevelopment_2)
        {
            ChangingDevelopment = true;
            _player_Progress.StrikeOnDeveloper = true;
            _player_Progress.SpeedEvolution += 0.01f;
            _player_Progress.MoneyPlus -= 0.6f;
            _player_Progress.money -= costDevelopment_2;
            costDevelopment_2 += 4;
        }
    }

    public void Development_3() //Military equipment
    {
        if (_player_Progress.money >= costDevelopment_3)
        {
            ChangingDevelopment = true;
            _player_Progress.StrikeOnDeveloper = true;
            if (_upgradeIconAbilitiesInt < 2)
                _upgradeIconAbilitiesInt += 1;
            _player_Progress.SpeedEvolution += 0.006f;
            _player_Progress.MoneyPlus -= 0.4f;
            _forceKick += 0.02f;
            _forceEcon += 0.5f;
            _forceDevelop += 0.002f;
            _player_Progress.money -= costDevelopment_3;
            costDevelopment_3 += 2;
        }
    }
    public void Attack_info_1()
    {
        _actionControllerPlayer.info.text = $"Hit the territory \nDamage -{_forceKick * 100} \n Price {costAttack_1}$";
    }
    public void Attack_info_2()
    {
        _actionControllerPlayer.info.text = $"Hit the economy \nDamage $-{_forceEcon} \n Price {costAttack_2}$";
    }
    public void Attack_info_3()
    {
        _actionControllerPlayer.info.text = $"Hit educational institutions \nDamage ↑-{_forceDevelop * 100} \n Price {costAttack_3}$";
    }
    public void Economy_ifon_1()
    {
        _actionControllerPlayer.info.text = $"Tax increase \nUpgrade {"$+1/↑-5"} \n Price {costEconomy_1}$";
    } 
    public void Economy_ifon_2()
    {
        _actionControllerPlayer.info.text = $"Increase the population \nUpgrade \n{"$+0.5/↑-0.2"} \n Price {costEconomy_2}$";
    } 
    public void Economy_ifon_3()
    {
        _actionControllerPlayer.info.text = $"Increase life expectancy \nUpgrade \n{"$+0.4/↑-0.1"} \n Price {costEconomy_3}$";
    }
    public void Development_ifon_1()
    {
        _actionControllerPlayer.info.text = $"Development of medicine \nUpgrade \n{"$-0.2/↑+0.5"} \n Price {costDevelopment_1}$";
    }
    public void Development_ifon_2()
    {
        _actionControllerPlayer.info.text = $"Development Сommerce \nUpgrade \n{"$-0.6/↑+1"} \n Price {costDevelopment_2}$";
    }
    public void Development_ifon_3()
    {
        _actionControllerPlayer.info.text = $"Develop military equipment \nUpgrade \n{"$-0.4/↑+0.6"} \n Price {costDevelopment_3}$";
    }
}
