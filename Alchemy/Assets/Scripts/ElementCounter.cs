using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementCounter : MonoBehaviour
{
    public List<AlchemyElement> ReachedElements;

    public void AddElement(string element)
    {
        Debug.Log(element);
        ReachedElements.Add(Resources.Load<GameObject>($"Prefabs/{element}").GetComponent<AlchemyElement>());
    }

}
