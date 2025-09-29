using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParedTouch : MonoBehaviour
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
    public void Disolver()
    {
        if (!_isDissolving)
        {
            _dissolve = 0f;
            _isDissolving = true;
            Debug.Log("Disolución iniciada");
        }
    }

    private void ObjectAction()
    {
        _dissolve += Time.deltaTime / dissolveDuration;
        mat.SetFloat("Disolver", _dissolve); 
        if (_dissolve >= 1f)
        {
            _dissolve = 1f;
            _isDissolving = false;
            StartCoroutine(DestroyAfterFrame());
        }
    }

    private IEnumerator DestroyAfterFrame()
    {
        yield return null;
        Destroy(gameObject); 
    }
}