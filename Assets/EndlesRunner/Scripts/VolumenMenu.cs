using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumenMenuu : MonoBehaviour
{


    public Slider volumeSlider;
    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("VolumenGlobal", 1f);

        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;

            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    public void ChangeVolume(float valor)
    {                                                                       //volumen persistente aunque cierres el jueguito
        AudioListener.volume = valor;                                       //volumen persistente aunque cierres el jueguito
        PlayerPrefs.SetFloat("VolumenGlobal", valor);                       //volumen persistente aunque cierres el jueguito
        PlayerPrefs.Save();                                                 //volumen persistente aunque cierres el jueguito
    }
}
