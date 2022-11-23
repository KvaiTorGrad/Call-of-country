using UnityEngine;

public class Bot_Progress : CountrySystem
{
    private ActionControllerBot actionControllerBot;
    private SystemBot _systemBot;
    protected override void Awake()
    {
        base.Awake();
        actionControllerBot = FindObjectOfType<ActionControllerBot>();
        _systemBot = FindObjectOfType<SystemBot>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void ProgressSystem()
    {
        if (progress != 100)
        {
            SystemsController.EvolutionIndicator[1].fillAmount += SpeedEvolution / 0.2f * Time.deltaTime;
            SystemsController.MoneyText[1].text = $"{money:0.00}$";
            SystemsController.EvolutionProgress[1].text = $"{progress:0}%";
            if (SystemsController.EvolutionIndicator[1].fillAmount >= 1)
            {
                evolution += 0.01f;
                progress += 1;
                money += MoneyPlus;
                SystemsController.EvolutionIndicator[1].fillAmount = 0;
                actionControllerBot.Bot_Analytics();
            }
        }
        else
        {
            if (IsGameActive)
            {
                IsGameActive = false;
                StartCoroutine(_systemBot.DestroyCountryPlayer());
            }
        }
        base.ProgressSystem();
    }
}
