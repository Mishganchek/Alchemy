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

    private Recipe _recipe;

    public Recipe Recipe => _recipe;


    public  UnityAction OpenDiscription;


    public void ChangeData( Sprite sprite, string text, Recipe recipe)
    {
        Image.sprite = sprite;
        Text.text = text;
        _recipe = recipe;
    }
}
