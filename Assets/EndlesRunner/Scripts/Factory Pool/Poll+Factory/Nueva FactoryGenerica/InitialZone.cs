using UnityEngine;

public class InitialZone : MonoBehaviour
{
    private bool _doOnce = false;

    [SerializeField]
    private Levels FirstLevel;

    private F_Generic<Levels> _Factorygeneric;

    private void Awake()
    {
        _Factorygeneric = FindAnyObjectByType<F_Generic<Levels>>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_doOnce) return;
        _doOnce = true;

        var NewLevel = _Factorygeneric.Create();
        NewLevel.SetPosition(FirstLevel.GetNextPosition);
    }
}
