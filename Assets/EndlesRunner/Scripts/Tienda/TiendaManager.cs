using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TiendaManager : MonoBehaviour, IScreen
{
    [Header("UI Tienda")]
    public GameObject tiendaPanel;
    public TMP_Text monedasTexto;  // ← MONEDAS VISIBLES EN LA TIENDA
    public GameObject itemSlotPrefab;
    public Transform contenidoPanel;

    [Header("Items")]
    public TiendaItem[] items;

    private bool tiendaAbierta = false;

    private void Start()
    {
        LlenarTienda();
        ActualizarMonedasUI(); // ← muestra monedas al entrar
    }

    public void AlternarTienda()
    {
        ScreenManager.Instance.ActivateScreen(this);
        ActualizarMonedasUI(); // ← refresca cuando se abre
    }

    private void LlenarTienda()
    {
        foreach (Transform child in contenidoPanel)
            Destroy(child.gameObject);

        foreach (TiendaItem item in items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, contenidoPanel);
            slot.transform.Find("Icono").GetComponent<Image>().sprite = item.icono;

            slot.GetComponent<Button>().onClick.AddListener(() => ComprarItem(item));
        }
    }

    private void ComprarItem(TiendaItem item)
    {
        int monedas = PlayerPrefs.GetInt("MonedasTotales", 0);

        if (monedas >= item.precio)
        {
            monedas -= item.precio;
            PlayerPrefs.SetInt("MonedasTotales", monedas);

            PlayerPrefs.SetInt(item.itemNombre + "_Comprado", 1);

            if (item.equipable)
            {
                // Equipar automáticamente usando el índice del item
                AudioShop.Instance.PlayMusic(item.clipIndex);

                // Guardar el índice seleccionado
                PlayerPrefs.SetInt("SelectedMusic", item.clipIndex);
                Debug.Log("Item index" + item.clipIndex);
            }

            PlayerPrefs.Save();
            Debug.Log("Compraste: " + item.itemNombre);

            ActualizarMonedasUI();
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }




    private void ActualizarMonedasUI()
    {
        int monedas = PlayerPrefs.GetInt("MonedasTotales", 0);
        monedasTexto.text = monedas.ToString();
    }

    public void Activate()
    {
        tiendaAbierta = true;
        tiendaPanel.SetActive(true);
        ActualizarMonedasUI();
    }

    public void Deactivate()
    {
        tiendaAbierta = false;
        tiendaPanel.SetActive(false);
    }
}

