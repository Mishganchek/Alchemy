using TMPro;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [SerializeField] private ElementCounter _elementCounter;
    [SerializeField] private RecipStorage _recipStorage;
    private TMP_Text _textComponent;
    Spawner _elementSpawner;

    void Awake()
    {
        _textComponent = GetComponent<TMP_Text>();
        _textComponent.color = Color.white;
        _elementSpawner = GetComponentInParent<Spawner>();
    }
    private void Start()
    {
        _textComponent.text = (_elementCounter.ReachedElements.Count + " элементов открыто из " + (_recipStorage.Templates.Length+4));
    }

    private void Update()
    {
        if (_elementSpawner.PanelAddElements.activeInHierarchy|| _elementSpawner.PanelAllElements.activeInHierarchy|| _elementSpawner.PanelHint.activeInHierarchy || _elementSpawner.MenuPanel.activeInHierarchy)
        {
            this.gameObject.transform.SetAsFirstSibling();
        }
        else
        {
            this.gameObject.transform.SetAsLastSibling();

        }

    }

    private void OnEnable()
    {
        _elementCounter.NewElement += OnValueChanged;
    }

    private void OnDisable()
    {
        _elementCounter.NewElement -= OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _textComponent.text = (value + " элементов открыто из "  +  (_recipStorage.Templates.Length+4));
    }
}
