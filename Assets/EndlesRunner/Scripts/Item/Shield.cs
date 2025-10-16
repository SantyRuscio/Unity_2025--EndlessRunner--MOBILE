using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{
    public override void Execute()
    {
        EventManager.Trigger(TypeEcvents.ShieldEvent);

        Destroy(gameObject);
    }
}
