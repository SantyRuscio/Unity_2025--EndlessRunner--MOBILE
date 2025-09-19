using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class Pool : MonoBehaviour
{
    [SerializeField] private uint initPoolSize;
    [SerializeField] private PooledObject objectToPool;

    private Stack<PooledObject> stack;

    private void Awake()
    {
        SetupPool();
    }

    private void SetupPool()
    {
        stack = new Stack<PooledObject>();

        PooledObject instance = null;

        for (int i = 0; i < stack.Count; i++)
        {
            instance = Instantiate(objectToPool);
            instance.Pool = this;
            instance.gameObject.SetActive(true);
            stack.Push(instance);
        }
    }

    public PooledObject GetPooledObject()
    {
        if(stack.Count == 0)
        {
            PooledObject newInstance = Instantiate(objectToPool);
            newInstance.Pool = this;
            return newInstance; 
        }
        PooledObject nextInstance = stack.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);   
    }

}
