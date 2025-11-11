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

    private float Effectduration;

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

        // Busca automáticamente todos los objetos que tengan scripts que hereden de Rewind
        rewinds = FindObjectsOfType<Rewind>();

        Debug.Log("Rewinds encontrados: " + rewinds.Length);
    }

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

    public float Speed => _speed;

    public PlayerModel GetPlayerModel()
    {
        return playerModel;
    }

    public void ShieldEventTimer(float duration)
    {
        Effectduration = duration;
        StartCoroutine(ShieldEffectTimer(Effectduration));
    }

    private IEnumerator ShieldEffectTimer(float Effectduration)
    {
        yield return new WaitForSeconds(Effectduration);

        Debug.Log("ENTRÉ A LA CORUTINA DEL ESCUDO - DESACTIVANDO");

        EventManager.Trigger(TypeEcvents.ShieldEndEvent);

    }

}


