using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AlchemyElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private string _elementName;

    public static event UnityAction<AlchemyElement, AlchemyElement> CollisionDetectedAction;
    public static event UnityAction<AlchemyElement> DoubleClickDetectedAction;

    private TMP_Text _title;

    private bool _isInteractable;
    private AlchemyElement _interactor;
    private float _lastClickTime;

    public string ElementName => _elementName;
    public bool IsInteractable => !_isInteractable;


    protected void Start()
    {
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        _title = GetComponentInChildren<TMP_Text>();
        _title.text = _elementName;
        _title.color = Color.white;
        _title.alignment = TextAlignmentOptions.Midline;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_isInteractable)
        {
            return;
        }

        AlchemyElement alchemyElement;

        if (!other.gameObject.TryGetComponent(out alchemyElement))
        {
            return;
        }

        _interactor = alchemyElement;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!_isInteractable)
        {
            return;
        }

        AlchemyElement alchemyElement;

        if (!other.gameObject.TryGetComponent(out alchemyElement))
        {
            return;
        }

        if (alchemyElement == _interactor)
        {
            _interactor = null;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        _isInteractable = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isInteractable = false;

        if (_interactor == null)
        {
            return;
        }

        CollisionDetectedAction?.Invoke(_interactor, this);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - _lastClickTime <= 0.3f)
        {
            DoubleClickDetectedAction?.Invoke(this);
        }

        _lastClickTime = Time.time;
    }
}




