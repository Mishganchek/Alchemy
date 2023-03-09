using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    [SerializeField] private GameObject _bookContent;
    [SerializeField] private GameObject _buttonPrefab;

    [field: SerializeField] public GameObject _discriptionPanel { get; private set; }
    [field: SerializeField] public TMP_Text DiscriptionText { get; private set; }

    private ElementCounter _elementCounter; 

    private void Awake()
    {
        DiscriptionText = _discriptionPanel.GetComponentInChildren<TMP_Text>();
        _elementCounter = gameObject.GetComponentInParent<ElementCounter>();
    }

    private void OnEnable()
    {
        Clear(_bookContent);

        foreach (var element in _elementCounter.ReachedElements.OrderBy(e => e.ElementName))
        {
            GameObject gameObject = Instantiate(_buttonPrefab, _bookContent.transform);

            gameObject.GetComponent<Image>().sprite = element.GetComponent<Image>().sprite;

            gameObject.GetComponentInChildren<TMP_Text>().text = element.ElementName;

        }
    }

    public void Clear(GameObject panel)
    {
        AlchemyElement[] elements = panel.GetComponentsInChildren<AlchemyElement>();

        foreach (AlchemyElement element in elements)
        {
            Destroy(element.gameObject);
        }
    }
}
