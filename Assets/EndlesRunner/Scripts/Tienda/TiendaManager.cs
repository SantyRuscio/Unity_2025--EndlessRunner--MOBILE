using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiendaManager : MonoBehaviour
{
    public GameObject tiendaPanel;
    public GameObject itemSlotPrefab;
    public Transform contenidoPanel; 

    public TiendaItem[] items; 

    private bool tiendaAbierta = false;

    private void Start()
    {
        LlenarTienda();
    }

    public void AlternarTienda()
    {
        tiendaAbierta = !tiendaAbierta;
        tiendaPanel.SetActive(tiendaAbierta);
    }

    void LlenarTienda()
    {
        foreach (Transform child in contenidoPanel)
        {
            Destroy(child.gameObject); // Limpiar antes
        }

        foreach (TiendaItem item in items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, contenidoPanel);
            slot.transform.Find("Icono").GetComponent<Image>().sprite = item.icono;

            // Podés agregar un tooltip o botón para comprar/equipar
            slot.GetComponent<Button>().onClick.AddListener(() => ComprarItem(item));
        }
    }

    void ComprarItem(TiendaItem item)
    {
        Debug.Log("Compraste: " + item.itemNombre);
        // Acá se puede restar monedas y agregar al inventario
    }
}
