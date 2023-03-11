using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AlchemyElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] protected string _elementName;
    [SerializeField] protected string _discriptions;

    public static UnityAction<AlchemyElement, AlchemyElement> OnCollisionDetectedAction;
    public static UnityAction<AlchemyElement> OnDoubleClickDetectedAction;


    protected TMP_Text _textComponent;
    protected TMP_Text _text;

    private Book _book;
    private PanelManager _panelManager;
    private bool _triger;
    private AlchemyElement _saveElement;
    private float _lastClickTime;

    public string Name { get; private set; }
    public string ElementName => _elementName;
    public bool IsInteractable => !_triger;


    protected void Start()
    {
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        _book = gameObject.GetComponentInParent<Book>();
        _textComponent = GetComponentInChildren<TMP_Text>();
        _textComponent.text = _elementName;
        _textComponent.color = Color.white;
        _textComponent.alignment = TextAlignmentOptions.Midline;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_triger)
        {
            return;
        }

        AlchemyElement alchemyElement;

        if (!other.gameObject.TryGetComponent<AlchemyElement>(out alchemyElement))
        {
            return;
        }

        _saveElement = alchemyElement;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!_triger)
        {
            return;
        }

        AlchemyElement alchemyElement;

        if (!other.gameObject.TryGetComponent<AlchemyElement>(out alchemyElement))
        {
            return;
        }

        if (alchemyElement == _saveElement)
        {
            _saveElement = null;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        _triger = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _triger = false;

        if (_saveElement == null)
        {
            return;
        }

        OnCollisionDetectedAction?.Invoke(_saveElement, this);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - _lastClickTime <= 0.8f)
        {
            OnDoubleClickDetectedAction?.Invoke(this);

        }

        _lastClickTime = Time.time;
    }
}




