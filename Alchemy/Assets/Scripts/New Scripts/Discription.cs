using UnityEngine;

[CreateAssetMenu(fileName = "Discription", menuName = "Alchemy/Discription", order = 52)]
public class Discription : ScriptableObject
{
    [field: SerializeField] public Sprite Ingridient1 { get; private set; }
    [field: SerializeField] public string Ingridient1NameForDiscription { get; private set; }
    [field: SerializeField] public string Ingridient1Name { get; private set; }
    [field: SerializeField] public Sprite Ingridient2 { get; private set; }
    [field: SerializeField] public string Ingridient2Name { get; private set; }
    [field: SerializeField] public string Ingridient2NameForDiscription { get; private set; }
    [field: SerializeField] public Sprite Result { get; private set; }
    [field: SerializeField] public string ResultNameForDiscription { get; private set; }
    [field: SerializeField] public string ResultName { get; private set; }
    [field: SerializeField] public string Discriptions{ get; private set; }
}
