using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelRecipe : MonoBehaviour
{
    [SerializeField] private ElementView _firstElement;
    [SerializeField] private ElementView _secondElement;
    [SerializeField] private ElementView _resultElement;
     
    private Hint _hint;

    private void Awake()
    {
        _hint = GetComponentInParent<Hint>();
    }

    private void OnEnable()
    {
        _firstElement.ChangeAppearance(_hint.Recipe.Discriptions.Ingridient1, _hint.Recipe.Ingridient1.ElementName); 
        _secondElement.ChangeAppearance(_hint.Recipe.Discriptions.Ingridient2, _hint.Recipe.Ingridient2.ElementName);  
        _resultElement.ChangeAppearance(_hint.Recipe.Discriptions.Result, _hint.Recipe.Result.ElementName);  
    }
}
