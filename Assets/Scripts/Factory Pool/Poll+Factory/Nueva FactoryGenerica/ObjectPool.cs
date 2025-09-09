using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ObjectPool<T>
{
        List<T> _stock = new List<T>();
        Func<T> _factory;
        Action<T> _turnOn;
        Action<T> _turnOff;

        public ObjectPool(Func<T> Factory, Action<T> TurnOff, Action<T> TurnOn, int initialStock = 5)
        {
            _factory = Factory;
            _turnOff = TurnOff;
            _turnOn = TurnOn;

            for (int i = 0; i < initialStock; i++)
            {
                var x = _factory();
                _turnOff(x);
                _stock.Add(x);
            }
        }

        public T Get()
        {
            if (_stock.Count > 0)
            {
                var x = _stock[0];
                _stock.RemoveAt(0);
                _turnOn(x);
                return x;
            }
            return _factory();
    }

        public void Return(T value)
        {
            _turnOff(value);
            _stock.Add(value);
        }

}

