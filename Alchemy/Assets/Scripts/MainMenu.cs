using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : GamePanel
{
   private void OnEnable()
    {
        gameObject.transform.SetAsLastSibling();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
