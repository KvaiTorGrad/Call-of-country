using UnityEngine;

public class SystemsControllerButtons : MonoBehaviour
{
    private ActionControllerPlayer actionControllerPlayer;
    private RectTransform pos;
    private void Awake()
    {
        actionControllerPlayer = FindObjectOfType<ActionControllerPlayer>();
        pos = GetComponent<RectTransform>();
        SimpleTouchController.TouchStateEvent += ActivationAndDeactivationButtonMenu;
    }

    private void ActivationAndDeactivationButtonMenu(bool isActiveController)
    {
        if (isActiveController)
        {
            if (pos.localPosition.x < 65)
                RealizationButton(true, false, false);
            else if (pos.localPosition.x > 65 && pos.localPosition.y > -65)
                RealizationButton(false, true, false);
            else if (pos.localPosition.x > 65 && pos.localPosition.y < -65)
                RealizationButton(false, false, true);
        }
        else
        {           
            actionControllerPlayer.CloseButton();
        }
    }
    private void RealizationButton(bool buttonZero, bool buttonOne, bool buttonTwo)
    {
        actionControllerPlayer.button[0].SetActive(buttonZero);
        actionControllerPlayer.button[1].SetActive(buttonOne);
        actionControllerPlayer.button[2].SetActive(buttonTwo);
    }
    private void OnDisable()
    {
        SimpleTouchController.TouchStateEvent -= ActivationAndDeactivationButtonMenu;
    }
}
