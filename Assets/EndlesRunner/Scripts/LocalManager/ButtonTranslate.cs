using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTranslate : MonoBehaviour
{
    public Lang Lang;

    public void BTN_ChangeLang()
    {
        LocalizationManager.instance.ChangeLang(Lang);
    }
}