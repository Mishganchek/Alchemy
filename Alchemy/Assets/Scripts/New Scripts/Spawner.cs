using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    [SerializeField] private RecipStorage _recipStorage;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private AlchemyElement[] _startElements;
    [SerializeField] private int _startGridSize;
    [SerializeField] private int _createGridSize;
    [SerializeField] private ClicArea _clicArea;
    [SerializeField] private Inventory _inventory;
    public GameObject PanelAddElements;
    public GameObject PanelAllElements;
    public GameObject PanelHint;
    public GameObject MenuPanel;

    public int ReachedCount => _reachedElements.Count;
    private List<AlchemyElement> _reachedElements;

    public List<AlchemyElement> ReachedElements => _reachedElements;

    public event UnityAction<int> NewElementCreated;


    private AlchemyElement _result;
    private RectTransform _rectTransform;
    private float _lastClickTime = 0f;
    private float _doubleClickTimeThreshold = 0.3f;
    private int _x = -860;
    private int _y = 240;


    private void OnEnable()
    {
       AlchemyElement.OnCollisionDetectedAction += OnElementsCollided;
        AlchemyElement.OnDoubleClickDetectedAction += CreateCopy;
        _clicArea.OnCliced += CreateFourElements;
        _inventory.OnAddButtonClicked += CreateNeedElements;

    }

    private void OnDisable()
    {
       AlchemyElement.OnCollisionDetectedAction -= OnElementsCollided;
        AlchemyElement.OnDoubleClickDetectedAction -= CreateCopy;
        _clicArea.OnCliced -= CreateFourElements;
        _inventory.OnAddButtonClicked -= CreateNeedElements;
    }
    private void Start()
    {
        _reachedElements = new List<AlchemyElement>();

        int i=0;

        foreach (var item in CreateGrid(_startGridSize*2,  new Vector2(-_startGridSize,-_startGridSize), new Vector2(2, 2)))
        {
            _rectTransform = Instantiate(_startElements[i].gameObject, transform).GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = item;
            i++;
        }
    }

    private void OnElementsCollided(AlchemyElement element1, AlchemyElement element2)
    {
        if (_recipStorage.TryFindRecipe(element1, element2, out _result))
        {
            AlchemyElement gameObject1 = Instantiate(_result, element2.transform.position, Quaternion.identity, _canvas.transform);
            Reach(_result);
            NewElementCreated?.Invoke(_reachedElements.Count);
            Destroy(element1.gameObject);
            Destroy(element2.gameObject);
        }
    }

    public void CreateFourElements(PointerEventData eventData)
    {

        if (Time.time - _lastClickTime <= _doubleClickTimeThreshold)
        {
            _rectTransform = _canvas.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, _canvas.worldCamera, out Vector2 clickPosition);

            int i = 0;

            foreach (var item in CreateGrid(_createGridSize*2, clickPosition +  new Vector2(-_createGridSize,-_createGridSize), new Vector2(2,2)))
            {                
                InstantiateElement(_startElements[i].gameObject, item);
                i++;
            }
        }

        _lastClickTime = Time.time;

    }

    private RectTransform InstantiateElement(GameObject elementPrefab, Vector2 position)
    {
        RectTransform elementRectTransform = Instantiate(elementPrefab, transform).GetComponent<RectTransform>();
        elementRectTransform.anchoredPosition = position;

        return elementRectTransform;
    }
    public void CreateNeedElements(IEnumerable<AlchemyElement> ChoisenElements)
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
        PanelAddElements.SetActive(false);

    }

    private void Reach(AlchemyElement alchemyElement)
    {
        if (!_reachedElements.Contains(alchemyElement))
        {
            _reachedElements.Add(alchemyElement);
        }
    }

    private void CreateCopy(AlchemyElement element)
    {
        Instantiate(element, element.transform.position, Quaternion.identity, _canvas.transform);
    }

    private IEnumerable<Vector2> CreateGrid(int step, Vector2 startPosition, Vector2 size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                yield return new Vector2(x * step, y * step) + startPosition;
            }
        }
    }

    public void AddElement(string name)
    {
        foreach (var recipe in _recipStorage.Templates)
        {
            if(name == recipe.Discriptions3.Name)
            {
                _reachedElements.Add(recipe.Result);
            }
        }
    }
}
