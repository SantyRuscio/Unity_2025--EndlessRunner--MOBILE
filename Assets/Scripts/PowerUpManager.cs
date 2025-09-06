using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("settings de PowerUps")]
    public float duracionEscudo = 5f;
    public float duracionIman = 5f;

    private bool tieneEscudo = false;
    private bool tieneIman = false;

    public static PowerUpManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        ItemEvents.OnPowerUpRecolectado += ActivarPowerUp;
    }

    void OnDisable()
    {
        ItemEvents.OnPowerUpRecolectado -= ActivarPowerUp;
    }

    private void ActivarPowerUp(PowerUp.TipoPowerUp tipo, float duracion)
    {
        switch (tipo)
        {
            case PowerUp.TipoPowerUp.Escudo:
                StartCoroutine(ActivarEscudo(duracion));
                break;

            case PowerUp.TipoPowerUp.Iman:
                StartCoroutine(ActivarIman(duracion));
                break;
        }

        Debug.Log($"PowerUp activado: {tipo} por {duracion} segundos");
    }

    private IEnumerator ActivarEscudo(float duracion)
    {
        tieneEscudo = true;
        Debug.Log("Escudo activado");

        yield return new WaitForSeconds(duracion);

        tieneEscudo = false;
        Debug.Log("Escudo desactivado");
    }

    private IEnumerator ActivarIman(float duracion)
    {
        tieneIman = true;
        Debug.Log("Imán activado - Atrayendo monedas");

        yield return new WaitForSeconds(duracion);

        tieneIman = false;
        Debug.Log("Imán desactivado");
    }

    public bool TieneEscudo()
    {
        return tieneEscudo;
    }

    public bool TieneIman()
    {
        return tieneIman;
    }

    // metodo para que obstaculos verifiquen si hay escudo
    public void VerificarDaño(int daño)
    {
        if (tieneEscudo)
        {
            Debug.Log("Escudo bloqueo el daño");
            return;
        }

        // Si no tiene escudo, aplicar daño al player

        // referencia para aplicar el daño

        Debug.Log($"Aplicando {daño} de daño al jugador");
    }
}
