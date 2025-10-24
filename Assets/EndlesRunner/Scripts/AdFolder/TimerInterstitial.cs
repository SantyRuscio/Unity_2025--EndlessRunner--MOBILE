using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerInterstitial : MonoBehaviour
{
    public float timeToAd;
    private float timer;

    private void Start()
    {
        timer = Time.time + timeToAd;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer)
        {
            AdsManager.Instance.ShowInterstitialAd();
            timer = Time.time + timeToAd;
        }
    }
}
