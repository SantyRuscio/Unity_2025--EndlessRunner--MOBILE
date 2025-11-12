using UnityEngine;
using System.Collections;

public class Coin : Item
{
    [Header("Variables")]
    public int valor = 1;

    private AudioSource _audioSource;
    private int valorOriginal;
    private Coroutine boostCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        valorOriginal = valor;
    }

    private void OnEnable()
    {
        EventManager.Subscribe(TypeEvents.MultiplierEvent, ActivarMultiplicador);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(TypeEvents.MultiplierEvent, ActivarMultiplicador);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.GetPlayerModel().transform.position);

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

        if (_audioSource != null)
            _audioSource.Play();

        Destroy(gameObject);
    }

    #region  Multiplicador temporal de valor de moneda
    private void ActivarMultiplicador(params object[] parameters)
    {
        float multiplicador = (float)parameters[0];
        float duracion = (float)parameters[1];

        if (boostCoroutine != null)
            StopCoroutine(boostCoroutine);

        boostCoroutine = StartCoroutine(MultiplicarValorTemporal(multiplicador, duracion));
    }

    private IEnumerator MultiplicarValorTemporal(float multiplicador, float duracion)
    {
        valor = Mathf.RoundToInt(valorOriginal * multiplicador);
        Debug.Log($" {name}: Valor de moneda aumentado a {valor} por {duracion} segundos.");
        yield return new WaitForSeconds(duracion);
        valor = valorOriginal;
        Debug.Log($" {name}: Valor de moneda volvió a {valorOriginal}.");
    }
    #endregion
}
