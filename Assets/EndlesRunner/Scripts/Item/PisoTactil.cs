using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisoTactil : Item
{
    [SerializeField] private Transform[] _movePoint;
    private int currentPointIndex = 0;
    [SerializeField] float _distance = 10f;
    [SerializeField] private float moveSpeed = 10f;

    private Material mat;

    //SHADDER
    private float _dissolve = 0f;
    public float dissolveDuration = 2f;   // Duraci�n del efecto
    [SerializeField] float dissolveSpeed = 0.5f; // velocidad de disoluci�n
    private bool _isDissolving = false;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Animaci�n de disoluci�n
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
            Debug.Log("Piso tocado me muevo al point");
            _isDissolving = true;
        }
    }
    private void ObjectAction()
    {
        if (_movePoint.Length == 0) return; 

        Transform targetPoint = _movePoint[currentPointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex++;

            if (currentPointIndex >= _movePoint.Length)
            {
                _isDissolving = false;
                currentPointIndex = _movePoint.Length - 1; 
            }
        }
    }
}
