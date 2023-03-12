using UnityEngine;

[CreateAssetMenu(fileName = "Ingridient", menuName = "Alchemy/Ingridient", order = 52)]
public class Ingridient : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string NameForDiscription { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

}
 