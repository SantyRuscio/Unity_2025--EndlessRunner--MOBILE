using System.Collections;
using UnityEngine;

public class Levels : Rewind
{
    private F_Generic<Levels> _Factorygeneric;

    [SerializeField]
    private float _positionToRetunrPool = -55f;

    [SerializeField]
    private Transform _nextPosition;

    [SerializeField]
    private Transform _itemPositionm;

    [SerializeField]
    private Transform _debufPosition;


    private bool _isStopped = false;

    public Vector3 GetNextPosition
    {
        get { return _nextPosition.position; }
        private set { }
    }

    private void Awake()
    {
        _Factorygeneric = FindAnyObjectByType<F_Generic<Levels>>();
        _state = new MementoState();
    }

    void Start()
    {
        EventManager.Subscribe(TypeEvents.GameOver, StopConstantMove);
        EventManager.Subscribe(TypeEvents.RewindEvent, StartConstantMoveRewind);
        EventManager.Subscribe(TypeEvents.Win, StopConstantMove);
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

        // PowerUp
        LevelsManager.instance.SpawnRandomPowerUp(_itemPositionm);

       
       // LevelsManager.instance.SpawnRandomDebuf(_debufPosition);
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
        pos.z -= GameManager.instance.Speed * Time.deltaTime;
        transform.position = pos;
    }

    private void StopConstantMove(params object[] parameters)
    {
        _isStopped = true;
    }

    private void StartConstantMoveRewind(params object[] parameters)
    {
        GameManager.instance.LoadMethod();

        GameManager.instance.StartCoroutine(TimeToStayReady());
    }

    private IEnumerator TimeToStayReady()
    {
        yield return new WaitForSeconds(GameManager.RewindControlDelay);
        _isStopped = false;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(TypeEvents.GameOver, StopConstantMove);
        EventManager.Unsubscribe(TypeEvents.RewindEvent, StartConstantMoveRewind);
        EventManager.Unsubscribe(TypeEvents.Win, StopConstantMove);
    }


    #region   //  SAVE Y LOAD DEL MEMENTO
    // =============================
    public override void Save()
    {
        _state.Rec(
            transform.position,
            transform.rotation,
            gameObject.activeSelf,
            _isStopped
        );
    }

    public override void Load()
    {
        if (!_state.IsRemembered())
        {
            return;
        }

        var x = _state.Remember();

        Vector3 savedPosition = (Vector3)x.parametres[0];
        Quaternion savedRotation = (Quaternion)x.parametres[1];
        bool savedActive = (bool)x.parametres[2];
        bool savedIsStopped = (bool)x.parametres[3];

        transform.position = savedPosition;
        transform.rotation = savedRotation;
        gameObject.SetActive(savedActive);
        _isStopped = savedIsStopped;
    }
    #endregion
}
