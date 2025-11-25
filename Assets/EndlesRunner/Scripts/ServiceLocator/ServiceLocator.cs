using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ServiceLocator
{
    public static ServiceLocator Instance => _instance ??= new ServiceLocator();

    private static ServiceLocator _instance;

   Dictionary<Type, object> _dependency = new Dictionary<Type, object>();


    public void RegisterDependency<T>(T service) 
    {
        var type = typeof(T);

        _dependency.TryAdd(type, service);
    }

    public T GetDependency<T>()
    {
        var type = typeof(T);

        if (_dependency.ContainsKey(type))
            return(T) _dependency[type];

        return default;
    }

   // public bool TryGetDependency<T>(out T service)
   // {
   //     var type = typeof(T);
   //
   //     if(_dependency.ContainsKey(type))
   //     {
   //         inter = (T)_dependency[type];
   //         return true;
   //     }
   //
   //     inter = default;
   //     return false;
   // }

    public void RemoveDependency<T>()
    {
        var type = typeof(T);

        if(_dependency.ContainsKey(type))
            _dependency.Remove(type);   
    }

}
