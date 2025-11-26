using System.Collections;
using UnityEngine;

public class Shield : PowerUp
{
  //  [SerializeField] private float duration = 10f; // si el dia de mañana se quiere usar otra duracion por cada power up esta abierto

    public override void Execute()
    {
        EventManager.Trigger(TypeEvents.ShieldEvent, GameManager.PowerUpDuration);
        
        SoundManager.Instance.PlaySFX(AudioClip);

        base.TriggerEvent();

        Destroy(gameObject);
    }
}