using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonFunction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _bookPanel;
    [SerializeField] private GameObject _addPanel;
    [SerializeField] private Inventory _inventory;

    public event UnityAction AddElement;


    public void OnPointerClick(PointerEventData eventData)
    {
        AddElement?.Invoke();
    }
}
