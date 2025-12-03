using System.Collections.Generic;

public enum TypeEvents
{
    GameOver,
    Win,
    ShieldEvent,
    ShieldEndEvent,
    RewindEvent,
    PowerUpImageSlot,
    MultiplierEvent,
    DefubCrabEvent
}

//IMPORTANTE DESSUSCRIBIRSE AL DESTRUIR 

public static class EventManager
{
    public delegate void Events(params object[] parameters);

    private static Dictionary< TypeEvents, Events> _events = new Dictionary<TypeEvents, Events>();

    public static void Subscribe(TypeEvents name , Events action)    //EventManager.Suscribe(TypeEvents.GameOver, (FUNCION QUE SE QUIERE CARGAR) )
    {
        if (!_events.ContainsKey(name))
        {
            _events.Add(name, action);
        }   
        else
        {
            _events[name] += action;
        }
    }

    public static void Unsubscribe(TypeEvents name, Events action)  //EventManager.UnSuscribe(TypeEvents.GameOver, (FUNCION QUE SE QUIERE SACAR) )
    {
        if (_events.ContainsKey(name))
        {
            _events[name] -= action;

            if (_events[name] == null)
                _events.Remove(name);
        }
    }

    public static void Trigger(TypeEvents name, params object[] parameters)  //EventManager.Trigger(TypeEvents.GameOver)
    {
        if (_events.ContainsKey(name))
        {
            _events[name]?.Invoke(parameters);
        }
    }
}
