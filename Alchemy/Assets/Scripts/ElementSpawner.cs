using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementSpawner : MonoBehaviour, IPointerClickHandler
{
    public GameObject PanelAddElements;
    public GameObject PanelAllElements;
    public GameObject PanelHint;
    public GameObject MenuPanel;

    private Canvas _canvas;
    private RectTransform _rectTransform;
    public List<AlchemyElement> ChoisenElements;

    private GameObject _water;
    private GameObject _fire;
    private GameObject _ground;
    private GameObject _air;

    private float _lastClickTime = 0f;
    private float _doubleClickTimeThreshold = 0.3f;
    AlchemyElement _gameObject;

    private int _x = -860;
    private int _y = 240;

    private void Start()
    {
        //_canvas = GetComponent<Canvas>();
        //_air = Resources.Load<GameObject>("Prefabs/Air");
        //_water = Resources.Load<GameObject>("Prefabs/Water");
        //_ground = Resources.Load<GameObject>("Prefabs/Ground");
        //_fire = Resources.Load<GameObject>("Prefabs/Fire");

        //_rectTransform = Instantiate(_fire, transform).GetComponent<RectTransform>();
        //_rectTransform.anchoredPosition = new Vector2(-160, 160);

        //_rectTransform = Instantiate(_water, transform).GetComponent<RectTransform>();
        //_rectTransform.anchoredPosition = new Vector2(160, -160);

        //_rectTransform = Instantiate(_ground, transform).GetComponent<RectTransform>();
        //_rectTransform.anchoredPosition = new Vector2(160, 160);

        //_rectTransform = Instantiate(_air, transform).GetComponent<RectTransform>();
        //_rectTransform.anchoredPosition = new Vector2(-160, -160);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PanelAddElements.activeInHierarchy|| PanelAllElements.activeInHierarchy|| PanelHint.activeInHierarchy)
        {
            return;
        }

        if (Time.time - _lastClickTime <= _doubleClickTimeThreshold)
        {
            Vector2 clickPosition = eventData.position;
            Vector2 localPosition = Vector2.zero;

            _rectTransform = _canvas.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, clickPosition, _canvas.worldCamera, out localPosition);

            InstantiateElement(_air, localPosition + new Vector2(-100, -100));
            InstantiateElement(_fire, localPosition + new Vector2(-100, 100));
            InstantiateElement(_water, localPosition + new Vector2(100, -100));
            InstantiateElement(_ground, localPosition + new Vector2(100, 100));
        }

        _lastClickTime = Time.time;
    }

    private void InstantiateElement(GameObject elementPrefab, Vector2 position)
    {
        RectTransform elementRectTransform = Instantiate(elementPrefab, transform).GetComponent<RectTransform>();
        elementRectTransform.anchoredPosition = position;
    }
    public void CreateNeedElements()
    {
        foreach (var element in ChoisenElements)
        {
            _gameObject = Instantiate(element, transform);
            _rectTransform = _gameObject.GetComponent<RectTransform>();
            _gameObject.transform.SetSiblingIndex(2);
            _rectTransform.anchoredPosition = new Vector2(_x, _y);

            if (_x < 860)
            {
                _x += 200;
            }
            else
            {
                _y -= 200;
                _x = -860;
            }
        }
        _x = -860;
        _y = 240;
        ChoisenElements.Clear();
        PanelAddElements.SetActive(false);

    }
}



