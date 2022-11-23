using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstCountry : MonoBehaviour
{
    MenuController menuController;
    SystemPlayer systemPlayer;
    public List<GameObject> m_count = new List<GameObject>();

    private int idFirst;
    public Dropdown CountriesFirst;
    private void Awake()
    {
        menuController = FindObjectOfType<MenuController>();
        systemPlayer = FindObjectOfType<SystemPlayer>();
    }
    public void FirstCountrie()
    {
        idFirst = CountriesFirst.value;
        menuController.Countries[1].options.RemoveAt(idFirst);
        menuController.Countries[0].options.Add(CountriesFirst.options[idFirst]);
        menuController.Search();
        m_count.Add(menuController.idCount_bot[idFirst]);
        menuController.idCount_bot.RemoveAt(idFirst);

        menuController.MenuLayer[0].SetActive(true);

        PlayerPrefs.SetInt("beginning", m_count.Count);
        systemPlayer.SaveCountSystems();

        menuController.MenuLayer[2].SetActive(false);

    }

}
