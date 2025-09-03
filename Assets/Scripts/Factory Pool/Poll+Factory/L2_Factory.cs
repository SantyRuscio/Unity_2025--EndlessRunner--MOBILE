using System;
using UnityEngine;

namespace Patterns.combined_Factory_Pool
{
    public class L2_Factory : MonoBehaviour
    {
        public static L2_Factory Instance { get; private set; }

        [SerializeField] private Levels[] _levelsPrefabs;   // Array de prefabs
        [SerializeField] private int _initialAmount;

        private Pool2M<Levels> _levelsPool;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // Crear el pool
            _levelsPool = new Pool2M<Levels>(
                () => Instantiate(_levelsPrefabs[0]),              // Instancia el primer prefab (puede ser random después)
                level => level.gameObject.SetActive(true),         // OnGet
                level => level.gameObject.SetActive(false),        // OnRelease
                _initialAmount
            );
        }

        //Obtiene un objeto del pool
        public Levels GetObjectFromPool()
        {
            return _levelsPool.GetObject();
        }

        // Devuelve un objeto al pool
        public void ReturnObjectToPool(Levels obj)
        {
            _levelsPool.ReturnObjectToPool(obj);
        }
    }
}

