
using System.Collections.Generic;

public class MementoState
{
   List <ParamsMemento> _data = new List<ParamsMemento> ();
    public void Rec(params object[] parameters)
    {
        if(_data.Count >= 1000)
          _data.RemoveAt(0);
        
        _data.Add(new ParamsMemento(parameters));

    }

    public bool IsRemembered()
    {
        return _data.Count > 0;
    }

    public void Delete()
    {
        _data.Clear();
    }

    public ParamsMemento Remember()
    {
        var x = _data[_data.Count - 1];

        _data.Remove(x);

        return x;
    }
}
