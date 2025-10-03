using UnityEngine;

public class Coin : Item
{
    [Header("Variables")]
    public int valor = 1;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
      // if (RemoteConfigExample.Instance != null)
      // {
      //     valor = RemoteConfigExample.Instance.coinsValue;
      // }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.GetPlayerModel().transform.position);

        if(!DetectionManager.instance)
        {
            print("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        // DetectionManager como dijo el profe
        if (distance < DetectionManager.instance.CurrentDistance())
        {
            Vector3 dir = (GameManager.instance.GetPlayerModel().transform.position - transform.position).normalized;

            transform.position += dir * DetectionManager.instance.CurrentSpeed() * Time.deltaTime;
        }
    }

    public override void Execute()
    {
        PuntuacionManager.Instance.AgregarMonedas(valor);

        _audioSource.Play();

        Destroy(gameObject);
    }
}
