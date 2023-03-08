using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private bool _isGivePanelOpen = false;
    private bool _isBookPanelOpen = false;

    public bool IsGivePanelOpen => _isGivePanelOpen;
    public bool IsBookPanelOpen => _isBookPanelOpen;

    public void OpenGivePanel(GameObject panel)
    {
        panel.SetActive(true);
        panel.transform.SetAsLastSibling();
        _isGivePanelOpen = true;
    }

    public void CloseGivePanel(GameObject panel)
    {
        panel.SetActive(false);
        _isGivePanelOpen = false;
    }

    public void OpenBookPanel(GameObject panel)
    {
        panel.SetActive(true);
        panel.transform.SetAsLastSibling();
        _isBookPanelOpen = true;
    }

    public void CloseBookPanel(GameObject panel)
    {
        panel.SetActive(false);
        _isBookPanelOpen = false;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
}
