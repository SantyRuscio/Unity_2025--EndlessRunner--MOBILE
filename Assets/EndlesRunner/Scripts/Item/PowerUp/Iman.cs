using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class Iman : PowerUp
{
    private float ChangeCoinDistance = 15f;
    private float ChangeCoinSpeed = 20f;
    private float ImanDuration = 10f;

    public override void Execute()
    {
        DetectionManager.instance.ActivateMagnet(ChangeCoinDistance, ChangeCoinSpeed, ImanDuration);

        ShaderManager.instance.ActivarPowerMode(ImanDuration);

        base.TriggerEvent();

        Destroy(gameObject);
    }
}
