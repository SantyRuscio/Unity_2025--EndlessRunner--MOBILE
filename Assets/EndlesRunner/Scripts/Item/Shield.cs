using UnityEngine;
using UnityEngine.UI;

public class Shield : Item
{
    [SerializeField] private GameObject _ShiedlImage;
    IScreen _ShiedlImageScreenRef;
    private float durationImage = 10f;

    void Start()
    {
        //_ShiedlImageScreenRef = _ShiedlImage.GetComponent<IScreen>();
    }

    public override void Execute()
    {
        EventManager.Trigger(TypeEcvents.ShieldEvent);

        //ScreenManager.Instance.ActivatePowerUpScreen(_ShiedlImageScreenRef, durationImage);

        Destroy(gameObject);
    }
}
