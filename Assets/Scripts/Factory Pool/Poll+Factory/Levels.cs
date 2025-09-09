using Patterns.combined_Factory_Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private F_Generic<Levels> _Factorygeneric;

    private void Awake()
    {
        _Factorygeneric = FindAnyObjectByType<F_Generic<Levels>>();
    }

    public void Initialize(F_Generic<Levels> Factory)
    {
        _Factorygeneric = Factory;
    }

    // Este método es solo para limpiar el objeto antes de volver al pool
    public void Realease()
    {
      //  _Factorygeneric.ReleaseLevel(this);  // si activop esta linea tira error pero no se reciclan las cosas

    }
}