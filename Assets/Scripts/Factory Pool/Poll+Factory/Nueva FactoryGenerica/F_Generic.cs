using UnityEngine;

public abstract class F_Generic<T> : MonoBehaviour
{
    public abstract T Create();

    public abstract void  ReleaseLevel(T level);
}