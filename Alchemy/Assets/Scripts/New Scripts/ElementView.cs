using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ElementView : MonoBehaviour
{
    [field: SerializeField] public Image Image { get; private set; }
    [field: SerializeField] public TMP_Text Text { get; private set; }

    public  UnityAction OpenDiscription;


    public void ChangeAppearance(Sprite sprite, string text,string name)
    {
        Image.sprite = sprite;
        Text.text = text;
        gameObject.name = name;
    }
}
