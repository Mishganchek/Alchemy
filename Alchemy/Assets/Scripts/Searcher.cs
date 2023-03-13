using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Searcher : MonoBehaviour
{
    [SerializeField] private TMP_InputField _input;

    private ElementView[] _elements;

    private void OnEnable()
    {
        _input.onValueChanged.AddListener(Render);
        _elements = GetComponentsInChildren<ElementView>();       
    }

    private void OnDisable()
    {
        _input.onValueChanged.RemoveListener(Render);
        _input.text = string.Empty;
    }

    private void Render(string text)
    {
        var selected = _elements
            .Where(element => element.Text.text.ToLower().StartsWith(text))
            .OrderBy(element => element.Text.text);

        print(selected.Count());

        foreach (var element in _elements)
        {
            element.gameObject.SetActive(false);
        }

        foreach (var element in selected)
        {
            element.gameObject.SetActive(true);
            element.gameObject.transform.SetAsLastSibling();
        }
    }
}
