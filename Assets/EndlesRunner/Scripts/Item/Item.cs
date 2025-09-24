using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string playerTag = "Player";

    public abstract void Execute();

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