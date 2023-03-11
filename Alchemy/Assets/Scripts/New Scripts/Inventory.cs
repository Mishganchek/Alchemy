using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] RectTransform _reachedElementsContent;
    [SerializeField] RectTransform _choisenElementsContent;
    [SerializeField] ElementView _button;
    [SerializeField] Spawner _spawner;
    [SerializeField] RecipStorage _recipStorage;

    private List<AlchemyElement> _choisenElements= new();

    public UnityAction <IEnumerable<AlchemyElement>> OnAddButtonClicked;

    public IEnumerable<AlchemyElement> ChoisenElements => _choisenElements;


    private void OnEnable()
    {
        Clear(_reachedElementsContent);

        foreach (var recipe in _recipStorage.SelectRecipes(_spawner.ReachedElements.OrderBy(e => e.ElementName)))
        {
            ElementView elementView = Instantiate(_button, _reachedElementsContent);
            elementView.ChangeData(recipe.Discriptions3.Sprite, recipe.Discriptions3.Name, recipe);
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
        OnAddButtonClicked?.Invoke(_choisenElements);
        ClosePanel();
        
    }

    public void ClosePanel()
    {
        _choisenElements.Clear();
        gameObject.SetActive(false);
    }

    public void ClearChoisen()
    {
        _choisenElements.Clear();
        Clear(_choisenElementsContent);
    }
}
