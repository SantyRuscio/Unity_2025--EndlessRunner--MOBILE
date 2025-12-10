using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverPrecio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")]
    public GameObject panelPrecio;
    public TMP_Text textoPrecio;

    [HideInInspector] public int precio; // Se asigna desde TiendaManager

    private void Start()
    {
        if (panelPrecio != null)
            panelPrecio.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panelPrecio != null)
        {
            textoPrecio.text = precio.ToString() + " $";
            panelPrecio.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (panelPrecio != null)
            panelPrecio.SetActive(false);
    }
}
