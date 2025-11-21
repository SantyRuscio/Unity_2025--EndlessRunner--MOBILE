
public class Iman : PowerUp
{
    private float ChangeCoinDistance = 15f;
    private float ChangeCoinSpeed = 20f;
    private float ImanDuration = 10f;

    public override void Execute()
    {
        SoundManager.Instance.PlaySFX(AudioClip);

        DetectionManager.instance.ActivateMagnet(ChangeCoinDistance, ChangeCoinSpeed, ImanDuration);

        ShaderManager.instance.ActivarPowerMode(ImanDuration);

        base.TriggerEvent();

        Destroy(gameObject);
    }
}
