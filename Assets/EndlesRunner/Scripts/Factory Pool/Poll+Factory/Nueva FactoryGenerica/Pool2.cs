using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Patterns.combined_Factory_Pool
{
    public class Pool2M<T>
    {
        //Delegado que dewvuelve T , aca se guarda el metodo de como se crea el obj
        private Func<T> _factoryMethod;

        //Delegados que toman por parametro tipo T , donde se ve como guardar, prender y apagar el obj una vez que se llame
        private Action<T> _turnOnCallBack;
        private Action<T> _turnOnOfflBack;

        //cajon donde guardo objetos para su uso
        private List<T> _currentStock;

        //constructor se llama cuando se crea una referencia de un pool nuevo
            
        public Pool2M (Func<T> factoryMethod, Action<T> turnOnCallBack , Action<T> turnOffCallBack, int initialAmount)
        {

            //iNICIAlizado en mi lista
            _currentStock = new List<T>();  

           //Guardo Como se crea el obj
            _factoryMethod = factoryMethod;

            //Guardo como se prende el obj
            _turnOnCallBack = turnOnCallBack;

            //Guardo como se apagael obj
            _turnOnOfflBack = turnOffCallBack;  


            for (int i = 0; i < initialAmount; i++)
            {
                //Uso delegado donde tenia guardado  el metodo para crear el obj
                T obj = _factoryMethod();   

                //Lo apago
                _turnOnOfflBack(obj);

                //Lo guardo en mi cajon
                _currentStock.Add(obj); 
            }


        }    

        public T GetObject() //Este es el metodo al que le piden una bala
        {
            T result;

            if( _currentStock.Count == 0)
            {
                result = _factoryMethod();  
            }
            else
            {
                result = _currentStock[0];
                _currentStock.RemoveAt(0);  
            }

            _turnOnCallBack(result);    
            return result;  
        }

        public void ReturnObjectToPool(T obj) //Este es el metodo que recibe la bala y la recicla
        {
            _turnOnOfflBack(obj);
            _currentStock.Add(obj);

        }
    }
}
