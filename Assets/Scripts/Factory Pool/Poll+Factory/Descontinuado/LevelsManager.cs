using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager instance;

    [SerializeField]
    private Transform _currentNextPosition;

    public Transform CurrentNextPosition
    {
        get { return _currentNextPosition; }
        set { _currentNextPosition = value; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}