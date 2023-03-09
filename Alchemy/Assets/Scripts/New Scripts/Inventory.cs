using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] private GameObject _reachedElementsConteiner;
    [SerializeField] private GameObject _chousenElementsConteiner;
    [SerializeField] private GameObject _giveElementsPanel;
    [SerializeField] private GameObject _buttonPrefab;

    public List<AlchemyElement> ChoisenElements;

    private ElementCounter _elementCounter;
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = gameObject.GetComponentInParent<Canvas>();
        _elementCounter = gameObject.GetComponentInParent<ElementCounter>();
    }

    private void Start()
    {
        //foreach (var element in _elementCounter.ReachedElements.OrderBy(e => e.ElementName))
        //{
        //    GameObject gameObject = Instantiate(_buttonPrefab, _reachedElementsConteiner.transform);

        //    gameObject.GetComponent<Image>().sprite = element.GetComponent<Image>().sprite;

        //    gameObject.GetComponentInChildren<TMP_Text>().text = element.ElementName;

        //}
    }

    public void CreateChoisen()
    {
        int x = -860;
        int y = 240;

        foreach (var element in ChoisenElements)
        {
            AlchemyElement alchemyElement = Instantiate(element, transform);
            RectTransform _rectTransform = alchemyElement.GetComponent<RectTransform>();
            alchemyElement.transform.SetParent(_canvas.transform);
            alchemyElement.transform.SetSiblingIndex(2);
            _rectTransform.anchoredPosition = new Vector2(x, y);

            if (x < 860)
            {
                x += 200;
            }
            else
            {
                y -= 200;
                x = -860;
            }
        }

        x = -860;
        y = 240;

        ChoisenElements.Clear();
    }
    public void Clear(GameObject panel)
    {
        AlchemyElement[] elements = panel.GetComponentsInChildren<AlchemyElement>();

        foreach (AlchemyElement element in elements)
        {
            Destroy(element.gameObject);
        }
    }

    public void AddInChoisen()
    {
        //Transform position = _chousenElementsConteiner.transform;
        //ChoisenElements.Add(prefab.GetComponent<AlchemyElement>());
        //Instantiate(prefab, position);
    }
}
