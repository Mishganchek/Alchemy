using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipStorage : MonoBehaviour
{
    [SerializeField] public Recipe[] Templates;

    private Dictionary<Tuple<string, string>, AlchemyElement> _recipies = new();

    private void Start()
    {
        foreach (var template in Templates)
        {
            _recipies.Add(Tuple.Create(template.Ingridient1.name, template.Ingridient2.name), template.Result);
        }
    }

    public bool CheckCollision(AlchemyElement alchemy1, AlchemyElement alchemy2, out AlchemyElement gameObject)
    {
        if (_recipies.TryGetValue(Tuple.Create(alchemy1.name, alchemy2.name), out gameObject))
        {
            return true;
        }
        if (_recipies.TryGetValue(Tuple.Create(alchemy2.name, alchemy1.name), out gameObject))
        {
            return true;
        }

        gameObject = null;
        return false;
    }
}
