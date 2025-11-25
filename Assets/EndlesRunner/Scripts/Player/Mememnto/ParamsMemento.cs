public class ParamsMemento
{
    public object[] parametres;

    public ParamsMemento(params object[] p)
    {
        parametres = new object[p.Length];
        for(int i = 0; i < parametres.Length; i++)
        {
            parametres[i] = p[i];   
        }
    }
}