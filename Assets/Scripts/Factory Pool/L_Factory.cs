using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Factory : Factory
{
    [SerializeField] private Levels[] _levelsPrefabs;

    public override IProduct GetProduct(Vector3 position)
    {
        Levels prefab = _levelsPrefabs[0];

        // Instanciar
        IProduct obj = Instantiate(prefab, position, Quaternion.identity);

        // Devolver como IProduct
        obj.Initialize();   

        return obj; 
    }
}

