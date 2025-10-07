using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TxtTranslate : MonoBehaviour
{
    [SerializeField] string _ID;
    TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        LocalizationManager.instance.EventTranslate += Translate;

        Translate();
    }

    void Translate()
    {
        _textMeshProUGUI.text = LocalizationManager.instance.GetTranslate(_ID);
    }
}
