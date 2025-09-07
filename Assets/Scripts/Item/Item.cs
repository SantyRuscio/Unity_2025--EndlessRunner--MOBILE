using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("settings")]

    [Tooltip("tag de lo que puede juntar este item")]
    public string playerTag = "Player";

    public abstract void Execute(GameObject jugador);

    public virtual void OnDestroy()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Execute(other.gameObject); 

            Destroy(gameObject);       
        }
    }
}