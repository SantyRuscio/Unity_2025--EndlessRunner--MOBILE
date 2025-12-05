using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager instance;

    [SerializeField] private Transform _currentNextPosition;

    [Header("Spawn Points")]
    [SerializeField] private Transform _spawnPoints;       
    [SerializeField] private Transform _debufSpawnPoints;  

    [Header("Factories")]
    [SerializeField] private ItemsFactory itemsFactory;
    [SerializeField] private DebufFactory debufFactory;

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

    public void SpawnRandomPowerUp(Transform spawnPoint)
    {
        var item = itemsFactory.TrySpawnItem(spawnPoint);

        if (item != null)
            item.transform.SetParent(spawnPoint);
    }


  // public void SpawnRandomDebuf(Transform spawnPoint)
  // {
  //     var debuf = debufFactory.TrySpawnDebuf(spawnPoint);
  //
  //     if (debuf != null)
  //         debuf.transform.SetParent(spawnPoint);
  // }

}

