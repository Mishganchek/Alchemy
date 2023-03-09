using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Book : MonoBehaviour
{
    [SerializeField] private GameObject _bookContent;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private AlchemyElement[] _startElements;

    [field: SerializeField] public GameObject _discriptionPanel { get; private set; }
    [field: SerializeField] public TMP_Text DiscriptionText { get; private set; }

    public event UnityAction<Recipe> OnDiscriptionsPanelOpen;

    private Spawner _spawner;
    private RecipStorage _recipStorage;

    private void Awake()
    {
        DiscriptionText = _discriptionPanel.GetComponentInChildren<TMP_Text>();
        _spawner = gameObject.GetComponentInParent<Spawner>();
        _recipStorage = gameObject.GetComponentInParent<RecipStorage>();
    }


    private void OnEnable()
    {
       Clear(_bookContent);

        foreach (var startElement in _startElements.OrderBy(e => e.ElementName))
        {
            ElementView elementView = Instantiate(_buttonPrefab, _bookContent.transform).GetComponent<ElementView>();
            elementView.ChangeAppearance(startElement.GetComponent<Image>().sprite, startElement.ElementName, startElement.name);
        }

        foreach (var element in _spawner.ReachedElements.OrderBy(e => e.ElementName))
        {
            ElementView elementView = Instantiate(_buttonPrefab, _bookContent.transform).GetComponent<ElementView>();
            ChangeData(elementView, element);
        }

        FindAllButtonBook();
    }

    public void Clear(GameObject panel)
    {
        ElementView[] elements = panel.GetComponentsInChildren<ElementView>();

        foreach (ElementView element in elements)
        {
            Destroy(element.gameObject);
        }
    }

    private void ChangeData(ElementView  elementView, AlchemyElement element)
    {
        foreach (var recipe in _recipStorage.Templates)
        {
            if (element.name == recipe.name)
            {
                elementView.ChangeAppearance(recipe.Discriptions.Result, recipe.Discriptions.ResultName,recipe.name);
            }
        }
    }

    private void FindAllButtonBook()
    {
        BookButton[] buttons = FindObjectsOfType<BookButton>();

        foreach (var button in buttons)
        {
            button.OnElementClicked += ShowDiscripsion;
        }
    }

    private void ShowDiscripsion(Recipe recipe)
    {
        _discriptionPanel.SetActive(true);

        _discriptionPanel.GetComponentInChildren<TMP_Text>().text = recipe.Discriptions.Discriptions;
        OnDiscriptionsPanelOpen?.Invoke(recipe);
         
    }
}
