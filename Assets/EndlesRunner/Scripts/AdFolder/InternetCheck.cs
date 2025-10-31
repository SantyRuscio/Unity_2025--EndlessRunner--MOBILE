using Unity.Services.RemoteConfig;
using UnityEngine;

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