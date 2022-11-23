using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject[] MenuLayer, allCountries;
    SystemsController systemsController;
    public Dropdown[] Countries;
    public int idPlayer, idBot;
    public Image[] countImage;
    public Image nextSpeedGame;
    public Sprite[] nextSpeedSprites;
    FirstCountry firstCountry;
    public List<GameObject> idCount_bot = new List<GameObject>();
    private int _speedGame;
    private float seaZoom, seaZoomPosX;
    private void Awake()
    {
        firstCountry = FindObjectOfType<FirstCountry>();
        systemsController = FindObjectOfType<SystemsController>();
    }

    void OnEnable()
    {
        FirstCount();
    }
    private void Start()
    {
        systemsController.Materials[3].SetFloat("ZoomUV_Zoom_1", seaZoom = 1.154f);
        systemsController.Materials[3].SetFloat("ZoomUV_PosX_1", seaZoomPosX = 0.092f);
        if (PlayerPrefs.HasKey("beginning"))
            Restor();
        _speedGame = 0;
    }
    public void PlayGame()
    {
        StartCoroutine(StartGame());
    }
    public void CountriesSelection()
    {
        idPlayer = Countries[0].value;
        idBot = Countries[1].value;
    }
    public void ResCount()
    {
        countImage[1].color = new Color(1, 1, 1, 1);
        countImage[0].color = new Color(1, 1, 1, 1);
        countImage[1].sprite = Countries[1].options[Countries[1].value].image;
        countImage[0].sprite = Countries[0].options[Countries[0].value].image;
    }

    public void FirstCount()
    {
        if (PlayerPrefs.HasKey("beginning"))
        {
            MenuLayer[0].SetActive(true);
            MenuLayer[1].SetActive(false);
            MenuLayer[2].SetActive(false);
            Countries[0].options.RemoveRange(0, Countries[0].options.Count);
            firstCountry.m_count.RemoveRange(0, firstCountry.m_count.Count);
            for (int i = 0; i < firstCountry.CountriesFirst.options.Count; i++)
            {
                for (int j = 0; j < firstCountry.CountriesFirst.options.Count; j++)
                {
                    if (firstCountry.CountriesFirst.options[i].text == PlayerPrefs.GetInt("CountrySave" + j).ToString())
                    {
                        Countries[0].options.Add(firstCountry.CountriesFirst.options[PlayerPrefs.GetInt("CountrySave" + j) - 1]);
                        firstCountry.m_count.Add(allCountries[PlayerPrefs.GetInt("CountrySave" + j) - 1]);
                    }
                }
            }
            for (int c = 0; c < Countries[0].options.Count; c++)
            {
                for (int c1 = 0; c1 < Countries[0].options.Count; c1++)
                {
                    if (Countries[0].options[c].text == Countries[0].options[PlayerPrefs.GetInt("CountrySave" + c1)].text)
                    {
                        Countries[0].options.Remove(Countries[0].options[PlayerPrefs.GetInt("CountrySave" + c1)]);
                        firstCountry.m_count.Remove(allCountries[PlayerPrefs.GetInt("CountrySave" + c1) - 1]);
                    }
                }
            }
        }
        else
        {
            MenuLayer[0].SetActive(false);
            MenuLayer[1].SetActive(false);
            MenuLayer[2].SetActive(true);
        }
    }

    public void Restor()
    {
        idCount_bot.RemoveRange(0, idCount_bot.Count);
        Countries[1].options.Clear();
        Countries[1].options.AddRange(firstCountry.CountriesFirst.options);
        Search();
    }
    public void NullIconVariable(int i)
    {
        if (i == 1)
        {
            countImage[0].color = new Color(0, 0, 0, 0);
            countImage[0].sprite = null;
        }
        if (i == 2)
        {
            countImage[1].color = new Color(0, 0, 0, 0);
            countImage[1].sprite = null;
        }
    }
    public void Search()
    {
        for (int j = 0; j < allCountries.Length; j++)
        {
            int n = Countries[0].value;
            {
                if (Countries[0].options[n].text == Countries[1].options[j].text)
                {
                    Countries[1].options.RemoveAt(Countries[1].value = j);
                    idCount_bot.AddRange(allCountries);
                    idCount_bot.RemoveAt(Countries[1].value = j);
                    Countries[1].value = 1;
                    break;
                }

            }
        }
    }

    private IEnumerator StartGame()
    {
        CountrySystem.IsGameActive = true;
        Countries[0].interactable = false;
        Countries[1].interactable = false;
        seaZoom = 1.154f;
        seaZoomPosX = 0.092f;
        systemsController.Materials[3].SetFloat("ZoomUV_Zoom_1", seaZoom = 1.154f);
        systemsController.Materials[3].SetFloat("ZoomUV_PosX_1", seaZoomPosX = 0.092f);
        while (seaZoom > 0.1f)
        {
            ResCount();
            systemsController.Materials[3].SetFloat("ZoomUV_Zoom_1", seaZoom -= 0.05f);
            systemsController.Materials[3].SetFloat("ZoomUV_PosX_1", seaZoomPosX += 0.03f);
            Countries[0].GetComponent<RectTransform>().offsetMin += new Vector2(0, -20);
            Countries[0].GetComponent<RectTransform>().offsetMax -= new Vector2(0, 20);
            Countries[0].GetComponent<RectTransform>().sizeDelta += new Vector2(1f, 1f);
            Countries[1].GetComponent<RectTransform>().offsetMin += new Vector2(0, 20);
            Countries[1].GetComponent<RectTransform>().offsetMax -= new Vector2(0, -20);
            Countries[1].GetComponent<RectTransform>().sizeDelta += new Vector2(1f, 1f);
            MenuLayer[3].transform.localPosition += new Vector3(0f, 50f, 0f);
            yield return new WaitForSeconds(0.06f);
        }
        MenuLayer[0].SetActive(false);
        MenuLayer[1].SetActive(true);
    }
    public IEnumerator EndGame()
    {
        Countries[0].interactable = true;
        Countries[1].interactable = true;
        seaZoom = 0.2f;
        seaZoomPosX = 0.7519997f;
        systemsController.Materials[3].SetFloat("ZoomUV_Zoom_1", seaZoom = 0.2f);
        systemsController.Materials[3].SetFloat("ZoomUV_PosX_1", seaZoomPosX = 0.7519997f);
        systemsController.Materials[1].SetFloat("_Destroyer_Value_1", 1);
        while (seaZoom < 1.10f)
        {
            ResCount();
            Countries[1].GetComponent<RectTransform>().sizeDelta = new Vector2(466.0f, 393.2f);
            Countries[0].GetComponent<RectTransform>().sizeDelta = new Vector2(437.98f, 393.1594f);
            systemsController.Materials[3].SetFloat("ZoomUV_Zoom_1", seaZoom += 0.05f);
            systemsController.Materials[3].SetFloat("ZoomUV_PosX_1", seaZoomPosX -= 0.03f);
            systemsController.Count.GetComponent<RectTransform>().sizeDelta -= new Vector2(1f, 1f);
            systemsController.Count.GetComponent<RectTransform>().sizeDelta -= new Vector2(1f, 1f);
            systemsController.Count.GetComponent<RectTransform>().offsetMax -= new Vector2(0, -20);
            systemsController.Count.GetComponent<RectTransform>().offsetMin += new Vector2(0, 20);
            systemsController.Count.GetComponent<RectTransform>().offsetMax -= new Vector2(0, 20);
            systemsController.Count.GetComponent<RectTransform>().offsetMin += new Vector2(0, -20);
            MenuLayer[3].transform.localPosition -= new Vector3(0f, 60f, 0f);
            yield return new WaitForSeconds(0.06f);
        }
        MenuLayer[3].transform.localPosition = new Vector3(315.0f, -15.0f, 0f);
        systemsController.Materials[3].SetFloat("ZoomUV_Zoom_1", seaZoom = 1.154f);
        systemsController.Materials[3].SetFloat("ZoomUV_PosX_1", seaZoomPosX = 0.092f);
        Countries[1].transform.localScale = Vector3.one;
        Countries[0].transform.localScale = Vector3.one;
        Countries[0].transform.localPosition = new Vector3(10.4f, -228.0f, 0);
        Countries[1].transform.localPosition = new Vector3(6.6f, 213.0f, 0);
        systemsController.Count.GetComponent<Image>().material = systemsController.Materials[0];
        systemsController.Count.GetComponent<Image>().material = systemsController.Materials[0];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseAndSpeedGameSet()
    {
        _speedGame++;
        switch (_speedGame)
        {
            case 0:
                nextSpeedGame.sprite = nextSpeedSprites[0];
                Time.timeScale = 2;
                break;
            case 1:
                nextSpeedGame.sprite = nextSpeedSprites[1];
                Time.timeScale = 3;
                break;
            case 2:
                nextSpeedGame.sprite = nextSpeedSprites[2];
                Time.timeScale = 0;
                break;
            case 3:
                nextSpeedGame.sprite = nextSpeedSprites[0];
                Time.timeScale = 2;
                _speedGame = 0;
                break;
        }
    }
    public void GoHome()
    {
        StartCoroutine(EndGame());
    }
}

