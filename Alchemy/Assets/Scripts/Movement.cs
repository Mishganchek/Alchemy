 
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform _rectTransform;
    private bool _isDragging;
    private Vector2 _offset;
    private PanelManager _panelManager;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _panelManager = GetComponentInParent<PanelManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _offset = eventData.position - (Vector2)_rectTransform.position;
        _isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_panelManager.IsGivePanelOpen|| _panelManager.IsBookPanelOpen)
        {
            return;
        }

        if (_isDragging)
        {
            _rectTransform.position = eventData.position - _offset;
            this.gameObject.transform.SetAsLastSibling();
        }
    }
}