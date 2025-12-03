using UnityEngine;
using System.Collections;

public class Coin : Item
{
    [Header("Variables")]
    public int valorMonedas = 1;
    [SerializeField] private AudioClip coinClip;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.GetPlayerModel().transform.position);

        if (distance < DetectionManager.instance.CurrentDistance())
        {
            Vector3 dir = (GameManager.instance.GetPlayerModel().transform.position - transform.position).normalized;
            transform.position += dir * DetectionManager.instance.CurrentSpeed() * Time.deltaTime;
        }
    }

    public override void Execute()
    {
        // Aplicar multiplicador solo desde PuntuacionManager
        PuntuacionManager.Instance.AgregarMonedas(valorMonedas);

        SoundManager.Instance.PlaySFX(coinClip);
        Destroy(gameObject);
    }
}

