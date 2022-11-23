using UnityEngine;
using TMPro;
public class ActionControllerPlayer : MonoBehaviour
{
    public TextMeshProUGUI info;
    public  GameObject[] button;
    private void Start()
    {
        CloseButton();
    }
    public void CloseButton()
    {
        button[0].SetActive(false);
        button[1].SetActive(false);
        button[2].SetActive(false);
        info.text = null;
    }
}
