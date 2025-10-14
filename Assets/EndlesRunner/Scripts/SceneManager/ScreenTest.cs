using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTest : MonoBehaviour, IScreen
{
    public GameObject nextScreen;

    public void BTN_OpenScreen()
    {
        if(nextScreen == null)  return;

        var screen = nextScreen.GetComponent<IScreen>();

        ScreenManager.Instance.ActivateScreen(screen);
    }
    public void Activate()
    {
       gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
