using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabDefub : Item
{
    [SerializeField] private AudioClip audioClip;

    public override void Execute()
    {

        if (audioClip != null)
            SoundManager.Instance.PlaySFX(audioClip);

        EventManager.Trigger(TypeEvents.ShieldEndEvent);
        EventManager.Trigger(TypeEvents.DefubCrabEvent);

        ResetObject();
    }

    public override void ResetObject()
    {
        base.ResetObject();
    }
}

