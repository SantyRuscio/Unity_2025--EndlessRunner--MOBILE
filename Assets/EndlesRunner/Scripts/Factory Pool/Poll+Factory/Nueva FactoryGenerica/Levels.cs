using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private F_Generic<Levels> _Factorygeneric;

    [SerializeField]
    private float _positionToRetunrPool = -55f;

    [SerializeField]
    private Transform _nextPosition;

    private bool _isStopped = false;

    public Vector3 GetNextPosition
    {
        get { return _nextPosition.position; }
        private set { }
    }

    private void Awake()
    {
        _Factorygeneric = FindAnyObjectByType<F_Generic<Levels>>();
    }

    void Start()
    {
        EventManager.Subscribe(TypeEcvents.GameOver, StopConstantMove);
    }

    private void Update()
    {
        if (!_isStopped)
        {
            ConstantMove();
        }

        if (transform.position.z <= _positionToRetunrPool)
        {
            ReturnLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_Factorygeneric != null)
        {
            _Factorygeneric.Create();
        }
    }

    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void Initialize(F_Generic<Levels> factory)
    {
        _Factorygeneric = factory;

        SetPosition(LevelsManager.instance.CurrentNextPosition.position);

        gameObject.SetActive(true);

        LevelsManager.instance.CurrentNextPosition = _nextPosition;
    }

    public void ResetObject()
    {
        gameObject.SetActive(false);
    }

    private void ReturnLevel()
    {
        if (_Factorygeneric != null)
        {
            _Factorygeneric.ReleaseLevel(this);
        }

        ResetObject();
    }

    public void ConstantMove()
    {
        Vector3 pos = transform.position;
        pos.z -= GameManager.instance.Speed * Time.deltaTime; //esto para que se muevan mas rapido
        transform.position = pos;
    }

    private void StopConstantMove(params object[] parameters)
    {
        _isStopped = true;
    }
}
