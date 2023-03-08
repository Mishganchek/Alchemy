using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscriptionPanel : MonoBehaviour
{
    [SerializeField] private ElementView _firstElement;
    [SerializeField] private ElementView _secondElement;
    [SerializeField] private ElementView _resultElement;

    private RecipStorage _recipStorage;
    private Recipe _currentRecipe;

    private void Awake()
    {
        _recipStorage = GetComponentInParent<RecipStorage>();

    }

    public void FindRecipe(string name)
    {
        foreach (var recipe in _recipStorage.Templates)
        {

            if (recipe.name == name)
            {
                _currentRecipe = recipe;
            }
        }

        _firstElement.ChangeAppearance(_currentRecipe.Discriptions.Ingridient1, _currentRecipe.Ingridient1.ElementName);
        _secondElement.ChangeAppearance(_currentRecipe.Discriptions.Ingridient2, _currentRecipe.Ingridient2.ElementName);
        _resultElement.ChangeAppearance(_currentRecipe.Discriptions.Result, _currentRecipe.Result.ElementName);
    }


}
