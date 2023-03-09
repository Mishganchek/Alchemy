using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelRecipe : MonoBehaviour
{
    [SerializeField] private ElementView _firstElement;
    [SerializeField] private ElementView _secondElement;
    [SerializeField] private ElementView _resultElement;

    private Book _book;

    private void Awake()
    {
        _book = gameObject.GetComponentInParent<Book>();
    }

    private void OnEnable()
    {
        _book.OnDiscriptionsPanelOpen +=ChangeApperans;
      
    }

    private void ChangeApperans(Recipe recipe)
    {

        _firstElement.ChangeAppearance(recipe.Discriptions.Ingridient1, recipe.Ingridient1.ElementName, recipe.Ingridient1.name);
        _secondElement.ChangeAppearance(recipe.Discriptions.Ingridient2, recipe.Ingridient2.ElementName, recipe.Ingridient2.name);
        _resultElement.ChangeAppearance(recipe.Discriptions.Result, recipe.Result.ElementName, recipe.Result.name);

    }
}
