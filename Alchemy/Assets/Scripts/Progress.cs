using TMPro;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [SerializeField] private ElementCounter _elementCounter;
    [SerializeField] private RecipStorage _recipStorage;
    [SerializeField] private Spawner _spawner;
    private TMP_Text _textComponent;

    void Awake()
    {
        _textComponent = GetComponent<TMP_Text>();
        _textComponent.color = Color.white;
        _spawner = GetComponentInParent<Spawner>();
    }
    private void Start()
    {
        _textComponent.text = (_spawner.ReachedCount + " элементов открыто из " + (_recipStorage.Templates.Length+4));
    }

    private void Update()
    {
        if (_spawner.PanelAddElements.activeInHierarchy|| _spawner.PanelAllElements.activeInHierarchy|| _spawner.PanelHint.activeInHierarchy || _spawner.MenuPanel.activeInHierarchy)
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
       _spawner.NewElementCreated += OnValueChanged;
    }

    private void OnDisable()
    {
        _spawner.NewElementCreated -= OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _textComponent.text = (value + " элементов открыто из "  +  (_recipStorage.Templates.Length+4));
    }
}
