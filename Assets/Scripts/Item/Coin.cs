using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    [Header("Asignaciones")]
    public AudioClip sonido;
    [SerializeField] private Transform _playerTr;

    [Header("Variables")]
    public int valor = 1;

    private float _distanceToMove = 2; // esto cambia al agarrar el iman
    private float _distanceToSpeed = 2; // esto cambia al agarrar el iman

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _playerTr.position);

        if (distance < _distanceToMove) 
        {
            Vector3 dir = (_playerTr.position - transform.position).normalized;

            transform.position += dir * _distanceToSpeed * Time.deltaTime; 
        }
    }

    public override void Execute()
    {
        PuntuacionManager.Instance.AgregarMonedas(valor);

        if (sonido != null)
            AudioSource.PlayClipAtPoint(sonido, transform.position);

        Destroy(gameObject);
    }

}
