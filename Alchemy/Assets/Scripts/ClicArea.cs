using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClicArea : MonoBehaviour, IPointerClickHandler
{
    public event UnityAction<PointerEventData> Clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(eventData);         
    }
}
