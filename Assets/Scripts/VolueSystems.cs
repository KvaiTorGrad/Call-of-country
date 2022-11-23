using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolueSystems : MonoBehaviour
{
   [SerializeField] private Image _but;
   [SerializeField] private Sprite[] _sprite;
    private bool _volueActive;

    void Start()
    {
        StartMusic();
    }

    public void StartMusic()
    {
                if (PlayerPrefs.HasKey("music"))
        {
            AudioListener.pause = PlayerPrefs.GetInt("music") == 1 ? true : false;
            _but.sprite = _sprite[PlayerPrefs.GetInt("music")];
        }
        else
{
    _but.sprite = _sprite[0];
}
    }
    public void MusClick()
    {

        if (_volueActive)
        {
            _but.sprite = _sprite[0];
            AudioListener.pause = false;
        }
        else
        {
            _but.sprite = _sprite[1];
            AudioListener.pause = true;
        }
        _volueActive = !_volueActive;
        PlayerPrefs.SetInt("music", _volueActive ? 1 : 0);
        PlayerPrefs.Save();
    }
}
