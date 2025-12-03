using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static float RewindControlDelay = 6f;
    public static float PowerUpDuration = 10f;


    private PlayerModel playerModel;
    public Rewind[] rewinds;

    [SerializeField] private float _speed = 10f;
    private float _defaultSpeed;

    public bool IsFromRewarded { get; set; } = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        playerModel = FindAnyObjectByType<PlayerModel>();
        _defaultSpeed = _speed;

        rewinds = FindObjectsOfType<Rewind>();

        Debug.Log("Rewinds encontrados: " + rewinds.Length);
    }

    private void Start()
    {
        EventManager.Subscribe(TypeEvents.ShieldEvent, ShieldEventTimer);

        AudioShop.Instance.LoadMusic();
    }

    #region MEMENTO 
    public void SaveMethod()
    {
        Debug.Log("Entre a SaveMethod");

        for (int i = 0; i < rewinds.Length; i++)
        {
            rewinds[i].Save();
            Debug.Log("Guardado estado de: " + rewinds[i].name);
        }
    }

    public void LoadMethod()
    {
        Debug.Log("Entro a LoadMethod");
        for (int i = 0; i < rewinds.Length; i++)
        {
            rewinds[i].Load();
        }
    }
    #endregion

    public float Speed => _speed;

    public PlayerModel GetPlayerModel()
    {
        return playerModel;
    }

    #region Events

    #region ShieldEventTimer
    public void ShieldEventTimer(params object[] parameters)
    {
        StartCoroutine(ShieldEffectTimer(PowerUpDuration));
    }

    private IEnumerator ShieldEffectTimer(float PowerUpDuration)
    {
        yield return new WaitForSeconds(PowerUpDuration);

        Debug.Log("ENTRÉ A LA CORUTINA DEL ESCUDO - DESACTIVANDO");

        EventManager.Trigger(TypeEvents.ShieldEndEvent);

    }
    #endregion

    #region CoinsMultiplier
    public void CoinMultiplierEventTimer(float duration)
    {
        PowerUpDuration = duration;
        StartCoroutine(CoinEffectTimer(PowerUpDuration));
    }

    private IEnumerator CoinEffectTimer(float PowerUpDuration)
    {
        yield return new WaitForSeconds(PowerUpDuration);

        // EventManager.Trigger(TypeEvents.ShieldEndEvent);
    }

    #endregion

    #endregion


    private void OnDestroy()
    {
        EventManager.Unsubscribe(TypeEvents.ShieldEvent, ShieldEventTimer);
    }

}


