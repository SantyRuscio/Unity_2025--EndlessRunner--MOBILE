using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParedTouch : Item, IObjectAction
{
    [SerializeField] float _distance = 10f;      // rango de detección Lo agregamos al remote
    [SerializeField] float dissolveSpeed = 0.5f; // velocidad de disolución
    private Material mat;

    private float _dissolve = 0f;
    public float dissolveDuration = 2f;   // Duración del efecto

    private bool _isDissolving = false;



    private void Start()
    {
       mat = GetComponent<Renderer>().material;

        if (RemoteConfigExample.Instance != null)
        {
            _distance = RemoteConfigExample.Instance.distanceToActivate;
        }
    }

    private void Update()
    {
        // Animación de disolución
        if (_isDissolving)
        {
            ObjectAction();
        }
    }

    public override void Execute()
    {
        StartShadder();
    }

    private void StartShadder()
    {
        if (!_isDissolving)
        {
            Debug.Log("Pared tocada, empezando disolución");
            _isDissolving = true;
        }
    }
    private void ObjectAction()
    {
        _dissolve += Time.deltaTime * dissolveSpeed;

        mat.SetFloat("Disolver", _dissolve);

        if (_dissolve >= 1f)
        {
            _isDissolving = false;
            Destroy(gameObject);
        }
    }
}