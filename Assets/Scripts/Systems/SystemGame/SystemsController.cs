using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemsController : MonoBehaviour
{
    [SerializeField] private GameObject _points;
    private Image _countImg;
    private GameObject _count;
    private MenuController _menuController;
    private VariableActionPlayer _variableActionPlayer;
    private ActionControllerBot _actionControllerBot;
    [SerializeField] private TextMeshProUGUI[] _moneyText, _evolutionProgress;
    [SerializeField] private Image[] _evolutionIndicator;
    [SerializeField] private Material[] _materials;
    private float _kickProgressEconomy, _kickProgressDevelopment;
    public GameObject Points { get => _points; set => _points = value; }
    public GameObject Count { get => _count; set => _count = value; }
    public TextMeshProUGUI[] MoneyText { get => _moneyText; set => _moneyText = value; }
    public TextMeshProUGUI[] EvolutionProgress { get => _evolutionProgress; set => _evolutionProgress = value; }
    public Image CountImg { get => _countImg; set => _countImg = value; }
    public MenuController MenuController { get => _menuController; set => _menuController = value; }
    public Material[] Materials { get => _materials; set => _materials = value; }
    public Image[] EvolutionIndicator { get => _evolutionIndicator; set => _evolutionIndicator = value; }
    public VariableActionPlayer VariableActionPlayer { get => _variableActionPlayer; set => _variableActionPlayer = value; }
    public ActionControllerBot ActionControllerBot { get => _actionControllerBot; set => _actionControllerBot = value; }

    protected virtual void Awake()
    {
        MenuController = FindObjectOfType<MenuController>();
        ActionControllerBot = FindObjectOfType<ActionControllerBot>();
        VariableActionPlayer = FindObjectOfType<VariableActionPlayer>();
    }
    protected virtual void StartGame()
    {
        Materials[1].SetFloat("_Destroyer_Value_1", 0);
    }
    protected virtual void Start() { }
    protected virtual void Update() { }

    protected virtual IEnumerator KickShagderTerritory()
    {
        Materials[1].SetFloat("_OutlineEmpty_Size_1", 0.2f);
        Materials[1].SetColor("_OutlineEmpty_Color_1", Color.red);
        yield return new WaitForSeconds(0.2f);
        Materials[1].SetFloat("_OutlineEmpty_Size_1", 0f);
        yield return new WaitForSeconds(0.2f);
        Materials[1].SetFloat("_OutlineEmpty_Size_1", 0.2f);
        yield return new WaitForSeconds(0.5f);
        Materials[1].SetFloat("_OutlineEmpty_Size_1", 0f);
        Materials[1].SetColor("_OutlineEmpty_Color_1", Color.black);
        Count.GetComponent<Image>().material = Materials[0];
        Count.GetComponent<Image>().material = Materials[0];
    }

    protected virtual IEnumerator KickShagderEconomy()
    {
        _kickProgressEconomy = 0;
        _kickProgressDevelopment = 1;
        Materials[1].SetFloat("_ClippingDown_Value_1", 0);
        Materials[1].SetFloat("_ClippingUp_Value_1", 1);
        if (ActionControllerBot._changinEgconomy || VariableActionPlayer.ChanginEgconomy)
            Materials[1].SetColor("_TintRGBA_Color_1", Color.red);
        else if (!ActionControllerBot._changinEgconomy || !VariableActionPlayer.ChanginEgconomy)
            Materials[1].SetColor("_TintRGBA_Color_1", Color.green);
        while (_kickProgressEconomy < 1f)
        {
            yield return new WaitForSeconds(0.1f);
            Materials[1].SetFloat("_ClippingDown_Value_1", _kickProgressEconomy += 0.05f);
        }
        while (_kickProgressDevelopment > 0f)
        {
            yield return new WaitForSeconds(0.1f);
            Materials[1].SetFloat("_ClippingUp_Value_1", _kickProgressDevelopment -= 0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        ActionControllerBot._changinEgconomy = false;
        VariableActionPlayer.ChanginEgconomy = false;
        Count.GetComponent<Image>().material = Materials[0];
        Count.GetComponent<Image>().material = Materials[0];
    }
    protected virtual IEnumerator KickShagderDeveloper()
    {
        if (!ActionControllerBot._changingDevelopment || !VariableActionPlayer.ChangingDevelopment)
            Materials[2].SetColor("_OutlineEmpty_Color_1", Color.red);
        if (ActionControllerBot._changingDevelopment || VariableActionPlayer.ChangingDevelopment)
            Materials[2].SetColor("_OutlineEmpty_Color_1", Color.green);
        Materials[2].SetFloat("_RGBA_Sub_Fade_1", 0f);
        yield return new WaitForSeconds(0.2f);
        Materials[2].SetFloat("_RGBA_Sub_Fade_1", 1f);
        yield return new WaitForSeconds(0.2f);
        Materials[2].SetFloat("_RGBA_Sub_Fade_1", 0f);
        yield return new WaitForSeconds(0.2f);
        Materials[2].SetFloat("_RGBA_Sub_Fade_1", 1f);
        yield return new WaitForSeconds(0.2f);
        Materials[2].SetFloat("_RGBA_Sub_Fade_1", 0f);
        yield return new WaitForSeconds(0.2f);
        Materials[2].SetFloat("_RGBA_Sub_Fade_1", 1f);
        EvolutionIndicator[0].material = null;
        EvolutionIndicator[1].material = null;
        ActionControllerBot._changingDevelopment = false;
        VariableActionPlayer.ChangingDevelopment = false;
    }
}
