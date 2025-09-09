using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class F_Generic<T> : MonoBehaviour
{
    public abstract T Create();

    public abstract void  ReleaseLevel(T level);
}