using UnityEngine;

public class InitialZone : MonoBehaviour, IPlatformsMove
{
    private bool _doOnce = false;

    float speed = 10f;

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
        float speed = 10f;

        Vector3 pos = transform.position;

        pos.z -= speed * Time.deltaTime;

        transform.position = pos;
    }
}
