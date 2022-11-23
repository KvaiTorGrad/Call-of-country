using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SystemBot : SystemsController
{
    private Bot_Progress bot_progress;
    protected override void Awake()
    {
        base.Awake();
        Points.AddComponent<Bot_Progress>();
        Points.tag = "Bot";
        bot_progress = Points.GetComponent<Bot_Progress>();
    }
    protected override void Start()
    {
        base.Start();
        CreateInGameCountry();
    }
    protected override void StartGame()
    {
        base.StartGame();
    }

    protected override void Update()
    {
        base.Update();

        bot_progress.ProgressSystem();
        CountImg.fillAmount = bot_progress.evolution;
        if (bot_progress.StrikeOnTerritory)
        {
            Count.GetComponent<Image>().material = Materials[1];
            StartCoroutine(KickShagderTerritory());
        }
        if (bot_progress.StrikeOnEconomick)
        {
            Count.GetComponent<Image>().material = Materials[1];
            StartCoroutine(KickShagderEconomy());
        }
        if (bot_progress.StrikeOnDeveloper)
        {
            EvolutionIndicator[1].material = Materials[2];
            StartCoroutine(KickShagderDeveloper());
        }

    }

    private void CreateInGameCountry()
    {
        Count = Instantiate(MenuController.idCount_bot[MenuController.idBot],
        Points.transform.position,
        MenuController.idCount_bot[MenuController.idBot].transform.rotation);
        Count.transform.parent = Points.transform;
        GameObject cou_bot = Count.transform.GetChild(0).gameObject;
        CountImg = cou_bot.GetComponentInChildren<Image>();
    }

    protected override IEnumerator KickShagderTerritory()
    {
        bot_progress.StrikeOnTerritory = false;
        return base.KickShagderTerritory();
    }
    protected override IEnumerator KickShagderEconomy()
    {
        bot_progress.StrikeOnEconomick = false;
        return base.KickShagderEconomy();
    }
    protected override IEnumerator KickShagderDeveloper()
    {
        bot_progress.StrikeOnDeveloper = false;
        return base.KickShagderDeveloper();
    }
    public IEnumerator DestroyCountryPlayer()
    {
        float progress = 0;
        while (progress < 1)
        {
            yield return new WaitForSeconds(0.1f);
            Materials[1].SetFloat("_Destroyer_Value_1", progress += 0.05f);
        }
        yield return null;
        StartCoroutine(MenuController.EndGame());
    }
}
