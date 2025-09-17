using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : Item
{
    private float ChangeCoinDistace = 15f;
    private float ChangeCoinSpeed = 20f;
    private float ImanDuration = 15f;
    public override void Execute()
    {
        DetectionManager.instance.ActivateMagnet(ChangeCoinDistace, ChangeCoinSpeed, ImanDuration);

        Destroy(gameObject);
    }
}
