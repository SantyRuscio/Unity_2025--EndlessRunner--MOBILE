

public class Shield : PowerUp
{
    private float duration = 10f;

    public override void Execute()
    {
        EventManager.Trigger(TypeEcvents.ShieldEvent, duration);

        base.TriggerEvent();

        Destroy(gameObject);
    }
}
