using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ElementButton : MonoBehaviour,IPointerClickHandler
{
    public event UnityAction<Recipe> ElementClicked;

    private Recipe _recipe;

    private void Start()
    {
        _recipe = gameObject.GetComponent<ElementView>().Recipe;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ElementClicked?.Invoke(_recipe);
    }
}
