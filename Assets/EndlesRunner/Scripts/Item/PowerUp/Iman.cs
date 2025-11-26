
public class Iman : PowerUp
{
    private float ChangeCoinDistance = 15f;
    private float ChangeCoinSpeed = 20f;
    //private float ImanDuration = 0f; // si el dia de mañana se quiere usar otra duracion por cada power up esta abierto

    public override void Execute()
    {
        SoundManager.Instance.PlaySFX(AudioClip);

        DetectionManager.instance.ActivateMagnet(ChangeCoinDistance, ChangeCoinSpeed, GameManager.PowerUpDuration);

        ShaderManager.instance.ActivarPowerMode(GameManager.PowerUpDuration);

        base.TriggerEvent();

        Destroy(gameObject);
    }
}
