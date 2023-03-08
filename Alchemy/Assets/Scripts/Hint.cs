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
        _recipes = _recipStorage.Templates.Where(recipe => (!_elementCounter.ReachedElements.Any(element => (element.name == recipe.name)))).OrderBy(recipe => recipe.Number);
    }

    private void OnEnable()
    {
        Recipe = _recipes.First();
        gameObject.transform.SetAsLastSibling();
        _text.text = $" ��������� ������� {Recipe.Discriptions.Name3} \n ����� ������� {Recipe.Discriptions.Name1} � {Recipe.Discriptions.Name2}";

        _firstElement.ChangeAppearance(Recipe.Discriptions.Ingridient1, Recipe.Ingridient1.ElementName);
        _secondElement.ChangeAppearance(Recipe.Discriptions.Ingridient2, Recipe.Ingridient2.ElementName);
        _resultElement.ChangeAppearance(Recipe.Discriptions.Result, Recipe.Result.ElementName);
    }
}
