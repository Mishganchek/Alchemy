using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Alchemy/Recipe", order = 51)]
public class Recipe : ScriptableObject
{
    [field: SerializeField] public AlchemyElement Ingridient1 { get; private set; }
    [field: SerializeField] public AlchemyElement Ingridient2 { get; private set; }
    [field: SerializeField] public AlchemyElement Result { get; private set; }
    [field: SerializeField] public Ingridient Discriptions1 { get; private set; }
    [field: SerializeField] public Ingridient Discriptions2 { get; private set; }
    [field: SerializeField] public Ingridient Discriptions3 { get; private set; }
    [field: SerializeField] public int Number { get; private set; }
    [field: SerializeField] public string Text { get; private set; }

}
