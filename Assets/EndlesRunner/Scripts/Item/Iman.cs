using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class Iman : PowerUp
{
    private float ChangeCoinDistace = 15f;
    private float ChangeCoinSpeed = 20f;
    private float ImanDuration = 15f;

    public override void Execute()
    {
        DetectionManager.instance.ActivateMagnet(ChangeCoinDistace, ChangeCoinSpeed, ImanDuration);

       // ScreenManager.Instance.ActivatePowerUpScreen(Iman, ImanDuration);

        ShaderManager.instance.ActivarPowerMode(ImanDuration);

        Destroy(gameObject);
    }
}
