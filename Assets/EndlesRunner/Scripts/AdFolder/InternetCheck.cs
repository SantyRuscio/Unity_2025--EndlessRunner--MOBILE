using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System;

public class InternetCheck : MonoBehaviour
{
    public GameObject message;

    void Start()
    {
        if (Utilities.CheckForInternetConnection())
        {
            Debug.Log("Internet");
            message.SetActive(false);
        }
    }
}