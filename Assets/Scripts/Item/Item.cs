using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

    public abstract void Execute(); // Logica de cada hijo en su codigo


    public virtual void OnDestroy() // Destroy en caso de que querramos algo que se ejcute al ser destruido 
    {

    }
}