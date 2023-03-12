using TMPro;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [SerializeField] private RecipStorage _recipStorage;
    [SerializeField] private Spawner _spawner;

    private TMP_Text _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<TMP_Text>();
        _textComponent.color = Color.white;
        _spawner = GetComponentInParent<Spawner>();
    }

    private void Start()
    {
        _textComponent.text = _spawner.ReachedElements.Count + " ��������� ������� �� " + _recipStorage.Templates.Count;
    }

    private void OnEnable()
    {
        _spawner.ElementCountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _spawner.ElementCountChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _textComponent.text = value + " ��������� ������� �� " + _recipStorage.Templates.Count;
    }
}
