using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementView : MonoBehaviour
{
    [field: SerializeField] public Image Image { get; private set; }
    [field: SerializeField] public TMP_Text Text { get; private set; }

    public void ChangeAppearance(Sprite sprite, string text)
    {
        Image.sprite = sprite;
        Text.text = text;
    }


}
