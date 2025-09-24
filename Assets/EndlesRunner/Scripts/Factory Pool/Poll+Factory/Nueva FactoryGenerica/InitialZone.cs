using UnityEngine;

public class InitialZone : MonoBehaviour
{
    private bool _doOnce = false;

    [SerializeField]
    private float _positionToRetunrPool = -55f;

    [SerializeField]
    private Levels FirstLevel;

    private F_Generic<Levels> _Factorygeneric;

    private void Awake()
    {
        _Factorygeneric = FindAnyObjectByType<F_Generic<Levels>>();
    }

    private void Update()
    {
        ConstantMove();
        Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_doOnce) return;
        _doOnce = true;

        var NewLevel = _Factorygeneric.Create();
        NewLevel.SetPosition(FirstLevel.GetNextPosition);
    }

    public void ConstantMove()
    {

        Vector3 pos = transform.position;

        pos.z -= GameManager.instance.Speed * Time.deltaTime;

        transform.position = pos;
    }

    private void Disable()
    {
        if (transform.position.z <= _positionToRetunrPool)
        {
            Destroy(gameObject);
        }
    }
}
