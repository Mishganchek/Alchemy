using UnityEngine;

public class PanelRecipe : MonoBehaviour
{
    [SerializeField] private ElementView _firstElement;
    [SerializeField] private ElementView _secondElement;
    [SerializeField] private ElementView _resultElement;

    [SerializeField] private Book _book;

    private void OnEnable()
    {
        if (_book == null)
        {
            return;
        }
        _book.DiscriptionsPanelOpened += ChangeApperans;

    }

    private void OnDisable()
    {
        if (_book == null)
        {
            return;
        }
        _book.DiscriptionsPanelOpened -= ChangeApperans;
    }

    public void ChangeApperans(Recipe recipe)
    {
        _firstElement.ChangeData(recipe.Discriptions1.Sprite, recipe.Ingridient1.ElementName, recipe);
        _secondElement.ChangeData(recipe.Discriptions2.Sprite, recipe.Ingridient2.ElementName, recipe);
        _resultElement.ChangeData(recipe.Discriptions3.Sprite, recipe.Result.ElementName, recipe);
    }
}
