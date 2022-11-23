using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Security.Cryptography;
using System;

public class SystemPlayer : SystemsController
{
    private FirstCountry firstCountry;
    private Player_Progress _player_Progress;
        [SerializeField] private Sprite[] _spriteImg;
    [SerializeField] private Image[] _upgradeImg;
    public Player_Progress Player_Progress { get => _player_Progress; set => _player_Progress = value; }

    protected override void Awake()
    {
        base.Awake();
        firstCountry = FindObjectOfType<FirstCountry>();
        Points.AddComponent<Player_Progress>();
        Points.tag = "Player";
        Player_Progress = Points.GetComponent<Player_Progress>();
    }
    protected override void Start()
    {
        base.Start();
        CreateInGameCountry();
    }
    protected override void StartGame()
    {
        base.StartGame();
        CreateInGameCountry();
    }

    protected override void Update()
    {
        base.Update();
            Player_Progress.ProgressSystem();
            CountImg.fillAmount = Player_Progress.evolution;
            if (Player_Progress.StrikeOnTerritory)
            {
                Count.GetComponent<Image>().material = Materials[1];
                StartCoroutine(KickShagderTerritory());
            }
            if (Player_Progress.StrikeOnEconomick)
            {
                Count.GetComponent<Image>().material = Materials[1];
                StartCoroutine(KickShagderEconomy());
            }
            if (Player_Progress.StrikeOnDeveloper)
            {
                EvolutionIndicator[0].material = Materials[2];
                StartCoroutine(KickShagderDeveloper());
            }
        UpdateIconAbilities();
    }

    private void CreateInGameCountry()
    {
        Count = Instantiate(firstCountry.m_count[MenuController.idPlayer],
        Points.transform.position,
        firstCountry.m_count[MenuController.idPlayer].transform.rotation);
        Count.transform.parent = Points.transform;
        GameObject cou_pl = Count.transform.GetChild(0).gameObject;
        CountImg = cou_pl.GetComponentInChildren<Image>();
        VariableActionPlayer.UpgradeIconAbilities = 0;
    }
    public void AddCountry()
    {
        var k = MenuController.Countries[0].options.Find(x => x.text == MenuController.Countries[1].options[MenuController.idBot].text);
        if (k != MenuController.Countries[1].options[MenuController.idBot])
        {
            MenuController.Countries[0].options.Add(MenuController.Countries[1].options[MenuController.idBot]);
            firstCountry.m_count.Add(MenuController.idCount_bot[MenuController.idBot]);
            SaveCountSystems();
        }
    }
    public void SaveCountSystems()
    {
        for (int i = 0; i < MenuController.Countries[0].options.Count; i++)
        {
            PlayerPrefs.SetInt("CountrySave" + i, Convert.ToInt32(MenuController.Countries[0].options[i].text));
            PlayerPrefs.Save();
        }
    }
    protected override IEnumerator KickShagderTerritory()
    {
        Player_Progress.StrikeOnTerritory = false;
        return base.KickShagderTerritory();
    } 
    protected override IEnumerator KickShagderEconomy()
    {
        _player_Progress.StrikeOnEconomick = false;
        return base.KickShagderEconomy();
    }
    protected override IEnumerator KickShagderDeveloper()
    {
        _player_Progress.StrikeOnDeveloper = false;
        return base.KickShagderDeveloper();
    } 
    public IEnumerator DestroyCountryBot()
    {
        float progress = 0;
        while (progress < 1)
        {
            yield return new WaitForSeconds(0.1f);
            Materials[1].SetFloat("_Destroyer_Value_1", progress += 0.05f);
        }
        yield return null;
        AddCountry();
        StartCoroutine(MenuController.EndGame());
    }
    private void UpdateIconAbilities()
    {
        switch (VariableActionPlayer.UpgradeIconAbilities)
        {
            case 0:
                _upgradeImg[0].sprite = _spriteImg[0];
                _upgradeImg[1].sprite = _spriteImg[1];
                _upgradeImg[2].sprite = _spriteImg[2];
                break;
            case 1:
                _upgradeImg[0].sprite = _spriteImg[3];
                _upgradeImg[1].sprite = _spriteImg[4];
                _upgradeImg[2].sprite = _spriteImg[5];
                break;
            case 2:
                _upgradeImg[0].sprite = _spriteImg[6];
                _upgradeImg[1].sprite = _spriteImg[7];
                _upgradeImg[2].sprite = _spriteImg[8];
                break;
            case 3:
                _upgradeImg[0].sprite = _spriteImg[9];
                _upgradeImg[1].sprite = _spriteImg[10];
                _upgradeImg[2].sprite = _spriteImg[11];
                break;
        }
    }
}
