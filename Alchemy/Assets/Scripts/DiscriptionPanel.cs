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

        _firstElement.ChangeData(_currentRecipe.Discriptions1.Sprite, _currentRecipe.Ingridient1.ElementName, _currentRecipe);
        _secondElement.ChangeData(_currentRecipe.Discriptions2.Sprite, _currentRecipe.Ingridient2.ElementName, _currentRecipe);
        _resultElement.ChangeData(_currentRecipe.Discriptions3.Sprite, _currentRecipe.Result.ElementName, _currentRecipe);
    }


}
