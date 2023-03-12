using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Book : GamePanel
{
    [SerializeField] private RectTransform _bookContent;
    [SerializeField] private ElementView _button;

    [field: SerializeField] public TMP_Text DiscriptionText { get; private set; }

    public event UnityAction<Recipe> DiscriptionsPanelOpened;

    private Spawner _spawner;
    private RecipStorage _recipStorage;

    private void Awake()
    {
        _spawner = gameObject.GetComponentInParent<Spawner>();
        _recipStorage = gameObject.GetComponentInParent<RecipStorage>();
    }

    private void OnEnable()
    {
       Clear(_bookContent);

        foreach (var recipe in _recipStorage.SelectRecipes( _spawner.ReachedElements.OrderBy(e => e.ElementName)))
        {
            ElementView elementView = Instantiate(_button, _bookContent);
            elementView.ChangeData(recipe.Discriptions3. Sprite, recipe.Discriptions3.Name, recipe);
        }

        FindAllButtons();
    }

    public void Clear(RectTransform panel)
    {
        ElementView[] elements = panel.GetComponentsInChildren<ElementView>();

        foreach (ElementView element in elements)
        {
            Destroy(element.gameObject);
        }
    }


    private void FindAllButtons()
    {
        ElementButton[] buttons = FindObjectsOfType<ElementButton>();

        foreach (var button in buttons)
        {
            button.ElementClicked += ShowDiscripsion;
        }
    }

    private void OnDisable()
    {
        ElementButton[] buttons = FindObjectsOfType<ElementButton>();

        foreach (var button in buttons)
        {
            button.ElementClicked -= ShowDiscripsion;
        }
    }

    private void ShowDiscripsion(Recipe recipe)
    {
        DiscriptionText.transform.parent.gameObject.SetActive(true);

        DiscriptionText.text = recipe.Text;
        DiscriptionsPanelOpened?.Invoke(recipe);     
    }
}
