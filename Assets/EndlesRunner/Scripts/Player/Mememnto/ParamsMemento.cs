public class ParamsMemento
{
    public object[] parametres;

    public ParamsMemento(params object[] p)
    {
        parametres = p;
    }
}