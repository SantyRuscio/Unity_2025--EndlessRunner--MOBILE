using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerModel playerModel;

    public Rewind[] rewinds;
    Coroutine _Coroutine;

    [SerializeField] private float _speed = 10f;
    private float _defaultSpeed;

    private Coroutine saveCoroutine;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        playerModel = FindAnyObjectByType<PlayerModel>();
        _defaultSpeed = _speed;


        Debug.Log("/////////////////" + rewinds.Length);
    }

    public void SaveMethod()
    {
        // foreach (var rewind in rewinds)
        //     rewind.Save();
        //
        // if (saveCoroutine != null)
        //     StopCoroutine(saveCoroutine);
        //
        // saveCoroutine = StartCoroutine(CoroutineSave());

        Debug.Log("Entre a saveMethod");

        for (int i = 0; i < rewinds.Length; i++)
        {
            rewinds[i].Save();
        Debug.Log("Entre al foreacvhj");
        }

        //StartCoroutine(CoroutineSave());
    }

    public void LoadMethod() 
    {
        //foreach (var rewind in rewinds) { rewind.Load(); }

        StartCoroutine(CoroutineLoad());
    }


    IEnumerator CoroutineSave()
    {
        Debug.Log("Entre a CoroutineSave");

        var WaitForSeconds = new WaitForSeconds(5f);

        while (true)
        {
            foreach (var rewind in rewinds)
                rewind.Save();

            yield return WaitForSeconds;
        }
    }

    IEnumerator CoroutineLoad()
    {
        var WaitForSeconds = new WaitForSeconds(.01f);
        bool isRemembered = true;

        while (isRemembered)
        {
            isRemembered = false;

            foreach (var rewind in rewinds)
            {
                rewind.Load();

                if (rewind.IsRemembered())
                {
                    isRemembered = true;
                }

                yield return WaitForSeconds;
            }
        }

        _Coroutine = StartCoroutine(CoroutineSave());
    }


    public float Speed
    {
        get { return _speed; }
        set { }
    }

    public PlayerModel GetPlayerModel()
    {
        return playerModel;
    }
}
