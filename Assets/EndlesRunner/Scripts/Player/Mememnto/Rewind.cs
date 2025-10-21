using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rewind : MonoBehaviour
{
    protected MementoState _state;
    private void Awake()
    {
        _state = new MementoState();
    }
    public bool IsRemembered() { return _state.IsRemembered(); }

    public abstract void Save();

    public abstract void Load();
}