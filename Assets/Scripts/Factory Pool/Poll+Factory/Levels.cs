using Patterns.combined_Factory_Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour, IProduct
{
    [SerializeField] private string _productName = "Leveles";
    public string ProductName
    {
        get
        {
            return _productName;
        }

    }

    public void Initialize()
    {
        Debug.Log("Level Created");
    }

    void Realease()
    {
        L2_Factory.Instance.ReturnObjectToPool(this);
    }
}
