using UnityEngine;
using System.Collections;

public class Coin : Item
{
    [Header("Variables")]
    public int valorMonedas = 1;

    [SerializeField] private AudioClip coinClip;

    private int valorOriginal;
    private Coroutine boostCoroutine;

    private void Awake()
    {
        valorOriginal = valorMonedas;
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
        PuntuacionManager.Instance.AgregarMonedas(valorMonedas);

        SoundManager.Instance.PlaySFX(coinClip);
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
        valorMonedas = Mathf.RoundToInt(valorOriginal * multiplicador);
        Debug.Log($" {name}: Valor de moneda aumentado a {valorMonedas} por {duracion} segundos.");
        yield return new WaitForSeconds(duracion);
        valorMonedas = valorOriginal;
        Debug.Log($" {name}: Valor de moneda volvió a {valorOriginal}.");
    }
    #endregion
}
