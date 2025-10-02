using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParedTouch : ObstaculosInteractuables
{
    [SerializeField] private float dissolveDuration = 1f; 
    private Material mat;
    private float _dissolve = 0f;
    private bool _isDissolving = false;

    private void Awake()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            mat = rend.material;
        }
        else
        {
            Debug.LogError("ParedTouch requiere un Renderer en el mismo GameObject");
        }
    }

    private void Update()
    {
        if (_isDissolving && mat != null)
            ObjectAction();
    }

    public override void ObjectAction()   //esto lo hice publico para que lo pueda overridear del padre
    {
        while (_dissolve < 1)
        {
            _dissolve += Time.deltaTime / dissolveDuration;
            mat.SetFloat("_Disolver", _dissolve);
          //  Debug.Log(_dissolve);

        }

        _dissolve = 1f;
        _isDissolving = false;
        StartCoroutine(DestroyAfterFrame());
    }
    
    private IEnumerator DestroyAfterFrame()
    {
        yield return null;
        Destroy(gameObject); 
    }

    public override void Execute()
    {
        if (!_isDissolving)
        {
            _dissolve = 0f;
            _isDissolving = true;
            ObjectAction();
            Debug.Log("Disolución iniciada");
        }
    }
}