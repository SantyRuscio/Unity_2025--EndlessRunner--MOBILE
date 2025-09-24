using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParedTouch : Item
{
    [SerializeField] float _distance = 10f;      // rango de detección
    [SerializeField] float dissolveSpeed = 0.5f; // velocidad de disolución

    private Material mat;

    private float _dissolve = 0f;
    public float dissolveDuration = 2f;   // Duración del efecto

    private bool _isDissolving = false;



    private void Start()
    {
       mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Animación de disolución
        if (_isDissolving)
        {
            DisolvAnim();
        }
    }


    public override void Execute()
    {
        StartDissolve();
    }

    private void StartDissolve()
    {
        if (!_isDissolving)
        {
            Debug.Log("Pared tocada, empezando disolución");
            _isDissolving = true;
        }
    }
    private void DisolvAnim()
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