using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    Stack <IScreen> _screens;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            DesactivateScreen();
    }

    public void ActivateScreen(IScreen screen)
    {
        screen.Activate();

        _screens.Push(screen);
    }

    public void DesactivateScreen()
    {
        if( _screens.Count > 0 )
        _screens.Pop().Deactivate();
    }
}
