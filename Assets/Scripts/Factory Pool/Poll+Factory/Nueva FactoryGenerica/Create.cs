using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public Transform NextLevelTransform;

    F_Generic<Levels> _Factorygeneric;

    private void Awake()
    {
        _Factorygeneric = FindAnyObjectByType<F_Generic<Levels>>();
    }

    private void OnTriggerEnter(Collider other)
    {

        
        
            Debug.Log("Entro Algo");

            //   var obj = L2_Factory.Instance.GetObjectFromPool();

            var b = _Factorygeneric.Create();

            Debug.Log("dd");

            //  obj.transform.position = NextLevelTransform.position;

            b.transform.position = NextLevelTransform.position;
       

    }
}
