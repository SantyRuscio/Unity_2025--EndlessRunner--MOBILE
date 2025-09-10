using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public static GameManager instance;
    private PlayerModel playerModel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        playerModel = FindAnyObjectByType<PlayerModel>();
    }

    public PlayerModel GetPlayerModel()
    {
        return playerModel;
    }
}