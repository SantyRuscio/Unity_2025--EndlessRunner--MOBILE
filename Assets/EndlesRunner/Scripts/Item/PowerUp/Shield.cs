using System.Collections;
using UnityEngine;

public class Shield : PowerUp
{
    [SerializeField] private float duration = 10f;

    public override void Execute()
    {
        EventManager.Trigger(TypeEvents.ShieldEvent, duration);
        
       SoundManager.Instance.PlaySFX(AudioClip);

        base.TriggerEvent();

        Destroy(gameObject);
    }
}