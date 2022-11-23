using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountrySystem : MonoBehaviour, IGameParametes
{
    private SystemsController _systemsController;
    private float _speedEvolution;
    private float _moneyPlus;
    private bool _strikeOnTerritory, _strikeOnEconomick, _strikeOnDeveloper;
    private static bool _isGameActive;
    public float evolution { set; get; }
    public float money { set; get; }
    public float progress { set; get; }

    public SystemsController SystemsController { get => _systemsController; private set => _systemsController = value; }
    public float SpeedEvolution { get => _speedEvolution; set => _speedEvolution = value; }
    public static bool IsGameActive { get => _isGameActive; set => _isGameActive = value; }
    public  bool StrikeOnTerritory { get => _strikeOnTerritory; set => _strikeOnTerritory = value; }
    public  bool StrikeOnEconomick { get => _strikeOnEconomick; set => _strikeOnEconomick = value; }
    public  bool StrikeOnDeveloper { get => _strikeOnDeveloper; set => _strikeOnDeveloper = value; }
    public  float MoneyPlus { get => _moneyPlus; set => _moneyPlus = value; }
    protected virtual void Awake()
    {
        SystemsController = FindObjectOfType<SystemsController>();
    }

    protected virtual void Start()
    {
        MoneyPlus = 1;
        SpeedEvolution = 0.01f;
    }
    public virtual void ProgressSystem()
    {
        Economica();
    }
    protected virtual void Economica()
    {
        if (evolution < 0)
        {
            evolution = 0;
            progress = 0;
        }
        if (SpeedEvolution <= 0.01f)
            SpeedEvolution = 0.01f;
        if (MoneyPlus <= 1)
            MoneyPlus = 1;
        if (money <= 0)
            money = 0;
    }
}
