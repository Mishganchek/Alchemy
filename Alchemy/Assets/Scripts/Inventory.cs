using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : GamePanel
{
    [SerializeField] private RectTransform _reachedElementsContent;
    [SerializeField] private RectTransform _choisenElementsContent;
    [SerializeField] private ElementView _button;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private RecipStorage _recipStorage;
    [SerializeField] private List<Recipe> _startRecipes;

    private List<AlchemyElement> _choisenElements= new();

    public UnityAction <IReadOnlyCollection<AlchemyElement>> AddButtonClicked;

    private void OnEnable()
    {
        foreach (var recipe in _startRecipes)
        {
            ElementView elementView = Instantiate(_button, _reachedElementsContent);
            elementView.ChangeData(recipe.Discriptions3.Sprite, recipe.Discriptions3.Name, recipe);
        }

        foreach (var recipe in _recipStorage.SelectRecipes(_spawner.ReachedElements.OrderBy(e => e.ElementName)))
        {
            ElementView elementView = Instantiate(_button, _reachedElementsContent);
            elementView.ChangeData(recipe.Discriptions3.Sprite, recipe.Discriptions3.Name, recipe);
        }

        FindAllButtons();
    }

    private void OnDisable()
    {
        _choisenElements.Clear();
        Clear(_reachedElementsContent);
        Clear(_choisenElementsContent);
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
            button.ElementClicked += AddInChoisen;
        }
    }

    private void AddInChoisen(Recipe recipe)
    {
        ElementView elementView = Instantiate(_button, _choisenElementsContent);
        elementView.ChangeData(recipe.Discriptions3.Sprite, recipe.Discriptions3.Name, recipe);
        _choisenElements.Add(recipe.Result);
    }

    public void AddOnTable()
    {
        AddButtonClicked?.Invoke(_choisenElements);
        Close();     
    }

    public void ClearChoisen()
    {
        _choisenElements.Clear();
        Clear(_choisenElementsContent);
    }
}
