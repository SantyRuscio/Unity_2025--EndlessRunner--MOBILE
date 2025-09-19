using System.Collections;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private F_Generic<Levels> _Factorygeneric;

    [SerializeField]
    private Transform _nextPosition;

    [SerializeField]
    private float _timerNextLevel = 9f;


    public Vector3 GetNextPosition 
    {
        get { return _nextPosition.position; } 
        private set { } 
    }

    private void Awake()
    {
        _Factorygeneric = FindAnyObjectByType<F_Generic<Levels>>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_Factorygeneric != null) 
        {
            _Factorygeneric.Create();
        }
    }

    public void SetPosition(Vector3 NewPos)
    {
        transform.position = NewPos;
    }

    public void Initialize(F_Generic<Levels> Factory)
    {
        _Factorygeneric = Factory;

        SetPosition(LevelsManager.instance.CurrentNextPosition.position);

        gameObject.SetActive(true);

        LevelsManager.instance.CurrentNextPosition = _nextPosition; 

        StartCoroutine(ReturnLevel());
    }

    public void ResetObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator ReturnLevel()
    {
        yield return new WaitForSeconds(_timerNextLevel);
            
        _Factorygeneric.ReleaseLevel(this);
    }
}