using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Country/Standart country", fileName = "New Country")]
public class CountryData : ScriptableObject
{
    [Tooltip("�������")]
    [SerializeField] private Sprite mainSpriteMateric;
    [Tooltip("����")]
    [SerializeField] private Sprite mainSpriteFlag;
    public Sprite MainSpriteMateric
    { get => mainSpriteMateric; 
    protected set { }
    }
    public Sprite MainSpriteFlag { get => mainSpriteFlag; 
    protected set { }
    }
}
