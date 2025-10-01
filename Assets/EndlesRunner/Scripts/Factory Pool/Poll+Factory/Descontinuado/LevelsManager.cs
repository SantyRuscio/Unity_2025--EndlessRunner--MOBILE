using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager instance;

    [SerializeField]
    private Transform _currentNextPosition;

    [SerializeField]
    private Transform _spawnPoints;

    [Header("Items Factory")]
    [SerializeField] private ItemsFactory itemsFactory;


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

    public void SpawnRandomPowerUp(Transform spawnPoints)
    {
        Debug.Log("entre a SpawnRandomPowerUp ");
        itemsFactory.TrySpawnItem(spawnPoints);
    }
}
