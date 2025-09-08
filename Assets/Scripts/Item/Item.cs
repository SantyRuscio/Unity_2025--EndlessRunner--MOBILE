using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("settings")]

    [Tooltip("tag de lo que puede juntar este item")]
    public string playerTag = "Player";

    protected float _spinSpeed;


    public abstract void Execute();

    public virtual void SpinConstant(float _spinSpeed)
    {
        transform.Rotate(Vector3.up * _spinSpeed * Time.deltaTime, Space.World);
    }

    public virtual void OnDestroy()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Execute(); 
        }
    }
}