using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamsMemento
{
    public object[] parametres;

    public ParamsMemento(params object[] p)
    {
        parametres = p;
    }
}