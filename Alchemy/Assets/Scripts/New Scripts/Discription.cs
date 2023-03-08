using UnityEngine;

[CreateAssetMenu(fileName = "Discription", menuName = "Alchemy/Discription", order = 52)]
public class Discription : ScriptableObject
{
    [field: SerializeField] public Sprite Ingridient1 { get; private set; }
    [field: SerializeField] public string Name1 { get; private set; }
    [field: SerializeField] public Sprite Ingridient2 { get; private set; }
    [field: SerializeField] public string Name2{ get; private set; }
    [field: SerializeField] public  Sprite Result { get; private set; }
    [field: SerializeField] public string Name3 { get; private set; }
}
