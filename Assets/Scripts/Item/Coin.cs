using UnityEngine;

public class Coin : Item
{
    [Header("Variables")]
    public int valor = 1;

    [SerializeField]
    private float _distanceToMove = 2; // esto cambia al agarrar el iman

    [SerializeField]
    private float _distanceToSpeed = 2; // esto cambia al agarrar el iman

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.GetPlayerModel().transform.position);

        if (distance < _distanceToMove) 
        {
            Vector3 dir = (GameManager.instance.GetPlayerModel().transform.position - transform.position).normalized;

            transform.position += dir * _distanceToSpeed * Time.deltaTime; 
        }
    }

    public override void Execute()
    {
        PuntuacionManager.Instance.AgregarMonedas(valor);

        _audioSource.Play();

        Destroy(gameObject);
    }
}