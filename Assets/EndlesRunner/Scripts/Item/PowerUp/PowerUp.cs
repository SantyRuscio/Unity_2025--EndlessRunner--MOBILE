using UnityEngine;
using System.Collections;

public abstract class PowerUp : Item
{
    [SerializeField, Tooltip("Sprite del PowerUp")]
    private Sprite sprite;

    [SerializeField, Tooltip("Sonido del PowerUp")]
    private AudioClip audioClip;

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    public AudioClip AudioClip  //es lo mismo que poner public AudioClip PowerUpSound => powerUpSound;
    {
        get
        {
            return audioClip;
        }
    }

    public override void Execute()
    { 
       TriggerEvent();
    }

    public virtual void TriggerEvent()
    {
        EventManager.Trigger(TypeEvents.PowerUpImageSlot, sprite);
    }
}

