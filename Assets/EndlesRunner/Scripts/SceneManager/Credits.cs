using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject _CredistScreen;
    IScreen _CredistScreenRef;

    void Start()
    {
        _CredistScreenRef = _CredistScreen.GetComponent<IScreen>(); 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            ScreenManager.Instance.ActivateScreen(_CredistScreenRef);
        }
    }
}
