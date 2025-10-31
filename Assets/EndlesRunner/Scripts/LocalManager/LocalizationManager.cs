using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    [SerializeField] Lang _language;

    [SerializeField] DatLocalization[] _data;

    Dictionary<Lang, Dictionary<string, string>> _translate = new Dictionary<Lang, Dictionary<string, string>>();

    public event Action EventTranslate;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            _translate = LenguageU.LoadTranslate(_data);

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void ChangeLang(Lang lang)
    {
        if (_language == lang) return;
        _language = lang;

        if (EventTranslate != null)
            EventTranslate();
    }

    public string GetTranslate(string ID)
    {
        if (!_translate.ContainsKey(_language))
        {
            return "No Lang";
        }
        if (!_translate [_language].ContainsKey(ID))
        {
            return "No ID";
        }

        return _translate[_language][ID];

    }
}
