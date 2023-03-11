using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] private PanelRecipe _hintRecipe;
    public GameObject Panel;
    private RecipStorage _recipStorage;
    private Spawner _spawner;
    private RectTransform _rectTransform;
    private TMP_Text _text;
    private IEnumerable<Recipe> _recipes;

    public event UnityAction<Recipe> OnHintPanelOpened;

    public Recipe Recipe { get; private set; }

    private void Awake()
    {
        _spawner = gameObject.GetComponentInParent<Spawner>();
        _recipStorage = gameObject.GetComponentInParent<RecipStorage>();
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _text = Panel.GetComponentInChildren<TMP_Text>();
        _recipes = _recipStorage.Templates.Where(recipe => (!_spawner.ReachedElements.Any(element => (element.name == recipe.name)))).OrderBy(recipe => recipe.Number);
    }

    private void OnEnable()
    {
        ShowDiscription();
    }

    private void ShowDiscription()
    {
        Recipe = _recipes.First();
        gameObject.transform.SetAsLastSibling();
        _hintRecipe.ChangeApperans(Recipe);
        _text.text = $" Ближайший элемент {Recipe.Discriptions3.NameForDiscription} \n Нужно смешать {Recipe.Discriptions1.NameForDiscription} и {Recipe.Discriptions2.NameForDiscription }";
    }
}
