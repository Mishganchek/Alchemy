using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementCounter : MonoBehaviour
{
    public List<AlchemyElement> ReachedElements;

    public event UnityAction<int> NewElement;

    RectTransform _rectTransform;
    private void Awake()
    {

        ReachedElements = new List<AlchemyElement>();
        //ReachedElements.Add(Resources.Load<GameObject>("Prefabs/Air").GetComponent<AlchemyElement>());
        //ReachedElements.Add(Resources.Load<GameObject>("Prefabs/Fire").GetComponent<AlchemyElement>());
        //ReachedElements.Add(Resources.Load<GameObject>("Prefabs/Ground").GetComponent<AlchemyElement>());
        //ReachedElements.Add(Resources.Load<GameObject>("Prefabs/Water").GetComponent<AlchemyElement>());

    }

    public void AddElement(string element)
    {
        Debug.Log(element);
        ReachedElements.Add(Resources.Load<GameObject>($"Prefabs/{element}").GetComponent<AlchemyElement>());
    }

    public void AddElement(AlchemyElement alchemyElement)
    {

        foreach (var item in ReachedElements)
        {
            if (item.name == alchemyElement.name)
            {
                return;
            }
        }

        //if (ReachedElements.Contains(alchemyElement))
        //{
        //    return;
        //}

        ReachedElements.Add(alchemyElement);
        NewElement?.Invoke(ReachedElements.Count);
    }
}
