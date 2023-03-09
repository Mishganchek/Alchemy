using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RecipStorage _recipStorage;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private ElementCounter _elementCounter;
    [SerializeField] private List<GameObject> _panels;
    public GameObject PanelAddElements;
    public GameObject PanelAllElements;
    public GameObject PanelHint;
    public GameObject MenuPanel;

    public event UnityAction<int> NewElement;

    public List<AlchemyElement> ChoisenElements;

    private AlchemyElement _result;
    private GameObject _water;
    private GameObject _fire;
    private GameObject _ground;
    private GameObject _air;
    private RectTransform _rectTransform;
    private float _lastClickTime = 0f;
    private float _doubleClickTimeThreshold = 0.3f;
    private int _x = -860;
    private int _y = 240;

    private void OnEnable()
    {

    }

    private void Start()
    {
        _air = Resources.Load<GameObject>("Prefabs/Air");
        _water = Resources.Load<GameObject>("Prefabs/Water");
        _ground = Resources.Load<GameObject>("Prefabs/Ground");
        _fire = Resources.Load<GameObject>("Prefabs/Fire");

        _rectTransform = Instantiate(_fire, transform).GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(-160, 160);

        _rectTransform = Instantiate(_water, transform).GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(160, -160);

        _rectTransform = Instantiate(_ground, transform).GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(160, 160);

        _rectTransform = Instantiate(_air, transform).GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(-160, -160);

        AddInReached(_air.GetComponent<AlchemyElement>());
        AddInReached(_water.GetComponent<AlchemyElement>());
        AddInReached(_ground.GetComponent<AlchemyElement>());
        AddInReached(_fire.GetComponent<AlchemyElement>());

        FindAllElements();
    }

    private void OnElementsCollided(AlchemyElement element1, AlchemyElement element2)
    {
        if (_recipStorage.CheckCollision(element1, element2, out _result))
        {
            AlchemyElement gameObject1 = Instantiate(_result, element2.transform.position, Quaternion.identity, _canvas.transform);
            AddInReached(_result.GetComponent<AlchemyElement>());
            NewElement?.Invoke(_elementCounter.ReachedElements.Count);
            Destroy(element1.gameObject);
            Destroy(element2.gameObject);

            FindAllElements();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var panel in _panels)
        {
            if (panel.activeInHierarchy)
            {
                return;
            }
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

        FindAllElements();
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
            AlchemyElement gameObject = Instantiate(element, transform);
            _rectTransform = gameObject.GetComponent<RectTransform>();
            gameObject.transform.SetSiblingIndex(2);
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
    private void FindAllElements()
    {
        AlchemyElement[] elements = FindObjectsOfType<AlchemyElement>();

        foreach (AlchemyElement element in elements)
        {
            element.OnCollisionDetectedAction -= OnElementsCollided;
            element.OnDoubleClickDetectedAction -= CreateCopy;
        }
        
        foreach (AlchemyElement element in elements)
        {
            element.OnCollisionDetectedAction += OnElementsCollided;
            element.OnDoubleClickDetectedAction += CreateCopy;
        }
    }

    private void AddInReached(AlchemyElement alchemyElement)
    {
        if (!_elementCounter.ReachedElements.Contains(alchemyElement))
        {
            _elementCounter.ReachedElements.Add(alchemyElement);
        }
    }

    private void CreateCopy(AlchemyElement element)
    {
        FindAllElements();

        AlchemyElement gameObject1 = Instantiate(element, element.transform.position, Quaternion.identity, _canvas.transform);

    }
}
