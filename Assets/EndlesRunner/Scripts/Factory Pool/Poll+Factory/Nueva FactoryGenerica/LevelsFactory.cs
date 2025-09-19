using Patterns.combined_Factory_Pool;
using UnityEngine;

public class LevelsFactory : F_Generic<Levels>
{
    public Levels[] prefabs;
    private Pool2M<Levels> _pool;

    [SerializeField]
    private int _initialAmount = 6;

    private void Awake()
    {
        // Creo el pool con los métodos que necesita (en el otro le pedi 4 cosas asi que le madno esas 4)
        _pool = new Pool2M<Levels>(
            CreatePrefab,
            InitilizeNewObject,
            DeactivateNewObject,
            _initialAmount
        );
    }

    public override Levels Create()
    {
        var x = _pool.GetObject();
        return x ;
    }

    Levels CreatePrefab()
    {
        var prefab = prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
        Levels b = Instantiate(prefab);
        return b;
    }

    private void InitilizeNewObject(Levels lvl)
    {
        lvl.Initialize(this);
    }

    private void DeactivateNewObject(Levels lvl)
    {
        lvl.ResetObject();
    }

    //Obtiene un objeto del pool
    public Levels GetLevel() 
    {
        return _pool.GetObject();
    }

    public override void ReleaseLevel(Levels level)
    {
        _pool.ReturnObjectToPool(level); 

    }
}
