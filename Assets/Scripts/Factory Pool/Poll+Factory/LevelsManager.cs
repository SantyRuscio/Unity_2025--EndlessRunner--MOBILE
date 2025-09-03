using Patterns.combined_Factory_Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public Transform MyTransform;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entro Algo");

        var obj = L2_Factory.Instance.GetObjectFromPool();

        obj.transform.position = MyTransform.forward;   
    }

}
