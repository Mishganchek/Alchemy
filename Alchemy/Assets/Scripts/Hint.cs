using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] private ElementView _resultElement;
    [SerializeField] private ElementView _firstElement;
    [SerializeField] private ElementView _secondElement;

    public GameObject Panel;
    private RecipStorage _recipStorage;
    private ElementCounter _elementCounter;
    private RectTransform _rectTransform;
    private TMP_Text _text;
    private IEnumerable<Recipe> _recipes;

    public Recipe Recipe { get; private set; }

    private void Awake()
    {
        _elementCounter = gameObject.GetComponentInParent<ElementCounter>();
        _recipStorage = gameObject.GetComponentInParent<RecipStorage>();
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _text = Panel.GetComponentInChildren<TMP_Text>();
        //_recipes = _recipStorage.Templates.Where(recipe => (!_elementCounter.ReachedElements.Any(element => (element.name == recipe.name)))).OrderBy(recipe => recipe.Number);
    }

    private void OnEnable()
    {
        Recipe = _recipes.First();
        gameObject.transform.SetAsLastSibling();
        _text.text = $" Ближайший элемент {Recipe.Discriptions.ResultNameForDiscription} \n Нужно смешать {Recipe.Discriptions.Ingridient1NameForDiscription} и {Recipe.Discriptions.Ingridient2NameForDiscription }";

        _firstElement.ChangeAppearance(Recipe.Discriptions.Ingridient1, Recipe.Ingridient1.ElementName,Recipe.Ingridient1.name);
        _secondElement.ChangeAppearance(Recipe.Discriptions.Ingridient2, Recipe.Ingridient2.ElementName,Recipe.Ingridient2.name);
        _resultElement.ChangeAppearance(Recipe.Discriptions.Result, Recipe.Result.ElementName,Recipe.Result.name);
    }
}
