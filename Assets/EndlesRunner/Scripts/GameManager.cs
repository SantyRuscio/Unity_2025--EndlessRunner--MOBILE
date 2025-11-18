using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerModel playerModel;
    public Rewind[] rewinds;

    [SerializeField] private float _speed = 10f;
    private float _defaultSpeed;

    private float Effectduration = 10f;

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
        StartCoroutine(ShieldEffectTimer(Effectduration));
    }

    private IEnumerator ShieldEffectTimer(float Effectduration)
    {
        yield return new WaitForSeconds(Effectduration);

        Debug.Log("ENTRÉ A LA CORUTINA DEL ESCUDO - DESACTIVANDO");

        EventManager.Trigger(TypeEvents.ShieldEndEvent);

    }
    #endregion

    #region CoinsMultiplier
    public void CoinMultiplierEventTimer(float duration)
    {
        Effectduration = duration;
        StartCoroutine(CoinEffectTimer(Effectduration));
    }

    private IEnumerator CoinEffectTimer(float Effectduration)
    {
        yield return new WaitForSeconds(Effectduration);

       // EventManager.Trigger(TypeEvents.ShieldEndEvent);
    }

    #endregion

    #endregion


    private void OnDestroy()
    {
      EventManager.Subscribe(TypeEvents.ShieldEvent, ShieldEventTimer);
    }
}
