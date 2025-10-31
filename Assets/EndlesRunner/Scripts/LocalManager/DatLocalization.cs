using UnityEngine;

[System.Serializable]
public struct DatLocalization
{
    public Lang language;
    public TextAsset[] data;
}

public enum Lang
{
    SPA,
    ENG
}