using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerModel playerModel;

    [SerializeField] private float _speed = 10f; // Valor por defecto
    private float _defaultSpeed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        playerModel = FindAnyObjectByType<PlayerModel>();
            
        _defaultSpeed = _speed;
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