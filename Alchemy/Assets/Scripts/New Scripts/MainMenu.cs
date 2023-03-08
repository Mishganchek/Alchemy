using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    void OnEnable()
    {
        gameObject.transform.SetAsLastSibling();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
