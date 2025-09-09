using Patterns.combined_Factory_Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField]
    private bool _isExitting;

    public Transform NextLevelTransform;

    [SerializeField]
    private Levels _myLevel;

    F_Generic<Levels> _Factorygeneric;

    private void Awake()
    {
        _Factorygeneric = FindAnyObjectByType<F_Generic<Levels>>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(!_isExitting)
        {
            Debug.Log("Entro Algo");

         //   var obj = L2_Factory.Instance.GetObjectFromPool();

            var b = _Factorygeneric.Create();

            Debug.Log("dd");

          //  obj.transform.position = NextLevelTransform.position;

            b.transform.position = NextLevelTransform.position;
        }
        else
        {
            // L2_Factory.Instance.ReturnObjectToPool(_myLevel);

            //_Factorygeneric.Return(_myLevel);

            Realease();
        }

        Destroy(gameObject);
    }

    private  void Realease()
    {
      //_Factorygeneric.ReleaseLevel(_myLevel); //Si prendo este lo apagas pero no lo devuelve
    }

}
