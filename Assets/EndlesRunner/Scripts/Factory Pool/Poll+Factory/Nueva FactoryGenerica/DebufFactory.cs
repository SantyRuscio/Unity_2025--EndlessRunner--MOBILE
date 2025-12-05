using Patterns.combined_Factory_Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebufFactory : MonoBehaviour
{
    [SerializeField] private Item[] debufPrefabs;
    [SerializeField] private int initialAmount = 5;
    [SerializeField, Range(0f, 1f)] private float spawnProbability = 0.3f;

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
        Item prefab = debufPrefabs[Random.Range(0, debufPrefabs.Length)];
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

    public Item TrySpawnDebuf(Transform spawnPoint)
    {
        if (spawnPoint == null) return null;

        if (Random.value <= spawnProbability)
        {
            Item item = _pool.GetObject();
            item.transform.position = spawnPoint.position;
            item.transform.rotation = spawnPoint.rotation;
            item.Initialize(this);

            return item;
        }

        return null;
    }
}

