using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelDebuf : Item
{
    [SerializeField] private AudioClip audioClip;

    public override void Execute()
    {
        Debug.Log("ENTRE AL CUBORUBIC");

        if (audioClip != null)
            SoundManager.Instance.PlaySFX(audioClip);

        ResetObject();
    }

    public override void ResetObject()
    {
        base.ResetObject();
    }
}
