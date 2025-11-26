using Patterns.combined_Factory_Pool;
using UnityEngine;

public class ItemsFactory : MonoBehaviour
{
    [Header("Items Prefabs")]
    [SerializeField] private Item[] prefabs; 

    [Header("Pool Settings")]
    [SerializeField] private int initialAmount = 10;

    [Header("Spawn Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float spawnProbability = 1f;  // posibilidad de que salga item

    private Pool2M<Item> _pool;

    private void Awake()
    {
        _pool = new Pool2M<Item>(
            CreatePrefab,
            InitializeNewObject,
            DeactivateObject,
            initialAmount
        );
    }

    private Item CreatePrefab()
    {
        Item prefab = prefabs[Random.Range(0, prefabs.Length)];
        return Instantiate(prefab);
    }

    private void InitializeNewObject(Item item)
    {
        item.Initialize(this);
    }

    private void DeactivateObject(Item item)
    {
        item.ResetObject();
    }

    public Item TrySpawnItem(Transform spawnPoint)
    {
        Debug.Log("entre a TrySpawnItem");

        if (spawnPoint == null) return null;     //ACA NO ESTARIA PASANDO ESTO 

        Debug.Log(" TrySpawnItem no es nulo ");

        if (Random.value <= spawnProbability)
        {
            Debug.Log("entre a spawnProbability");

            Item item = _pool.GetObject();

            item.transform.position = spawnPoint.position;
            item.transform.rotation = spawnPoint.rotation;

            item.Initialize(this);

            return item;
        }

        return null;
    }


    public void ReleaseItem(Item item)
    {
        _pool.ReturnObjectToPool(item);
    }
}