using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClicArea : MonoBehaviour, IPointerClickHandler
{
    public UnityAction<PointerEventData> OnCliced;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCliced?.Invoke(eventData);         
    }
}
