using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementDestroyer : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<AlchemyElement>(out AlchemyElement alchemyElement))
        {
            if (alchemyElement.IsInteractable)
            {

                Destroy(collider.gameObject);
            }
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Wait);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Wait);
    }

    private void Wait()
    {
        StartCoroutine(Deselct());
    }
    private IEnumerator Deselct()
    {
        yield return new WaitForSeconds(0.4f);
        _button.OnDeselect(null);
    }

    public void DestroyElements()
    {
        AlchemyElement[] alchemyElements = FindObjectsOfType<AlchemyElement>();

        foreach (var alchemyElement in alchemyElements)
        {
            Destroy(alchemyElement.gameObject);
        }
    }
}
