using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BookButton : MonoBehaviour,IPointerClickHandler
{
    private string _discription;
    private RecipStorage _recipStorage;

    public event UnityAction<Recipe> OnElementClicked;

    private void Start()
    {
        _recipStorage = gameObject.GetComponentInParent<RecipStorage>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnElementClicked?.Invoke(FindRecipe());
    }

    private Recipe FindRecipe()
    {
        foreach (var recipe in _recipStorage.Templates)
        {
            if(gameObject.name == recipe.name)
            {
                return recipe;
            }
        }

        return null;
    }

    public void TakeDiscription(string discription)
    {
        _discription = discription;
    }
}
