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
        _screens = new Stack<IScreen>();
    }

    public void ActivatePowerUpScreen(IScreen screen, float duration)
    {
        Debug.Log("ENTRE A ActivatePowerUpScreen ");

        screen.Activate();

        _screens.Push(screen);

        StartCoroutine(ImageDuration(duration));
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

    private IEnumerator ImageDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        DesactivateScreen();
    }
}
