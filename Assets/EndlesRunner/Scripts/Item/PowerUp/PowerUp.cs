using UnityEngine;
using System.Collections;

public abstract class PowerUp : Item
{
    [SerializeField, Tooltip("Sprite que representa este PowerUp")]
    private Sprite sprite;

    public Sprite Sprite => sprite;

    public override void Execute()
    {
        TriggerEvent();
    }

    public virtual void TriggerEvent()
    {
        EventManager.Trigger(TypeEvents.PowerUpImageSlot, sprite);
    }
}
