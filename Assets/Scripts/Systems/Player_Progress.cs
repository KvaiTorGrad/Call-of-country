using UnityEngine;
using UnityEngine.UIElements;

public class Player_Progress : CountrySystem
{
    private SystemPlayer _sysytemPlayer;
    public delegate void GameOver();
    public static event GameOver Over;
    protected override void Awake()
    {
        base.Awake();
        _sysytemPlayer = FindObjectOfType<SystemPlayer>();
    }

    protected override void Start()
    {
        base.Start();
    }
    public override void ProgressSystem()
    {
        if (progress != 100)
        {
            SystemsController.EvolutionIndicator[0].fillAmount += SpeedEvolution / 0.2f * Time.deltaTime;
            SystemsController.MoneyText[0].text = $"{money:0.00}$";
            SystemsController.EvolutionProgress[0].text = $"{progress:0}%";
            if (SystemsController.EvolutionIndicator[0].fillAmount >= 1)
            {
                evolution += 0.01f;
                progress += 1;
                money += MoneyPlus;
                SystemsController.EvolutionIndicator[0].fillAmount = 0;
            }
        }
        else
        {
            if (IsGameActive)
            {
                IsGameActive = false;
                StartCoroutine(_sysytemPlayer.DestroyCountryBot());
                Over();
            }
        }
        base.ProgressSystem();
    }

}
