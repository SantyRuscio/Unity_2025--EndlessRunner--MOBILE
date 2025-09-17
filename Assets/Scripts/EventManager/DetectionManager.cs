using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    public static DetectionManager instance;

    [SerializeField] private float _distanceToMove = 2f; //ESTAS VARIABLES SE CAMBIAN AL AGARRAR EL IMAN
    [SerializeField] private float _distanceToSpeed = 2f; //ESTAS VARIABLES SE CAMBIAN AL AGARRAR EL IMAN

    private void Awake()
    {
        instance = this;
    }

    public float CurrentDistance()
    {
        return _distanceToMove;
    }

    public float CurrentSpeed()
    {
        return _distanceToSpeed;
    }
}