using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ElementButton : MonoBehaviour,IPointerClickHandler
{
    private RecipStorage _recipStorage;

    [SerializeField] private Recipe _recipe;

    public event UnityAction<Recipe> ElementClicked;

    private void Start()
    {
        _recipStorage = gameObject.GetComponentInParent<RecipStorage>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ElementClicked?.Invoke(gameObject.GetComponent<ElementView>().Recipe);
    }
}
