using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    [SerializeField] private RecipStorage _recipStorage;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private AlchemyElement[] _startElements;
    [SerializeField] private ClicArea _clicArea;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Grid _basicElements;
    [SerializeField] private Grid _table;

    private List<AlchemyElement> _reachedElements = new();
    private AlchemyElement _result;
    private RectTransform _rectTransform;
    private float _lastClickTime = 0f;
    private float _doubleClickTimeThreshold = 0.3f;

    public IReadOnlyCollection<AlchemyElement> ReachedElements => _reachedElements;

    public event UnityAction<int> ElementCountChanged;

    private void OnEnable()
    {
        AlchemyElement.CollisionDetectedAction += OnElementsCollided;
        AlchemyElement.DoubleClickDetectedAction += CreateCopy;
        _clicArea.Clicked += CreateFourElements;
        _inventory.AddButtonClicked += CreateNeedElements;

    }

    private void OnDisable()
    {
        AlchemyElement.CollisionDetectedAction -= OnElementsCollided;
        AlchemyElement.DoubleClickDetectedAction -= CreateCopy;
        _clicArea.Clicked -= CreateFourElements;
        _inventory.AddButtonClicked -= CreateNeedElements;
    }
    private void Start()
    {
        int i = 0;

        foreach (var item in  _basicElements)
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
            ElementCountChanged?.Invoke(_reachedElements.Count);
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

            foreach (var item in _basicElements)
            {
                InstantiateElement(_startElements[i].gameObject, new Vector2( item.x+clickPosition.x, item.y +clickPosition.y));
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

    public void CreateNeedElements(IReadOnlyCollection<AlchemyElement> ChoisenElements)
    {
        IEnumerator<Vector2> enumerator = _table.GetEnumerator();

        foreach (var element in ChoisenElements)
        {
            enumerator.MoveNext();
            AlchemyElement gameObject = Instantiate(element, transform);
            _rectTransform = gameObject.GetComponent<RectTransform>();
            gameObject.transform.SetSiblingIndex(2);
            _rectTransform.anchoredPosition = enumerator.Current;
        }
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

    public void AddElement(string name)
    {
        Debug.Log("¬ходим в цикл");
        foreach (var recipe in _recipStorage.Templates)
        {
            Debug.Log(name);
            Debug.Log(recipe.name);

            if (name == recipe.name)
            {
                _reachedElements.Add(recipe.Result);
            }
        }
    }

    public void ClearProgress()
    {
        _reachedElements.Clear();
        DestroyAllElements();
        ElementCountChanged?.Invoke(_reachedElements.Count);
    }

    private void DestroyAllElements()
    {
        AlchemyElement[] elements = FindObjectsOfType<AlchemyElement>();

        foreach (var element in elements)
        {
            Destroy(element.gameObject);
        }
    }
}
    