using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TiendaManager : MonoBehaviour, IScreen
{
    [Header("UI Tienda")]
    public GameObject tiendaPanel;
    public TMP_Text monedasTexto;  // MONEDAS VISIBLES EN LA TIENDA
    public GameObject itemSlotPrefab;
    public Transform contenidoPanel;

    [Header("Items")]
    public TiendaItem[] items;

    [Header("Referencias")]
    [SerializeField] private StaminaSystemWithNotifications staminaSystem;


    private bool tiendaAbierta = false;

    private void Start()
    {
        if (staminaSystem == null)return;

        LlenarTienda();
        ActualizarMonedasUI(); //muestra monedas al entrar
    }

    public void AlternarTienda()
    {
        ScreenManager.Instance.ActivateScreen(this);
        ActualizarMonedasUI(); //refresca cuando se abre
    }

    private void LlenarTienda()
    {
        foreach (Transform child in contenidoPanel)
            Destroy(child.gameObject);

        foreach (TiendaItem item in items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, contenidoPanel);

            // Icono
            slot.transform.Find("Icono").GetComponent<Image>().sprite = item.icono;

            // Hover precio
            HoverPrecio hover = slot.GetComponent<HoverPrecio>();
            if (hover != null)
                hover.precio = item.precio;

            // Si ya fue comprado, aplicamos estilo pero SIN desactivar el botón
            bool comprado = PlayerPrefs.GetInt(item.itemNombre + "_Comprado", 0) == 1;
            if (comprado)
            {
                AplicarEstiloComprado(slot);
            }

            // Botón comprar/seleccionar
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

            int itemIndex = Array.IndexOf(items, item);

            // SUMAR STAMINA DE A 1 (ITEM 5)
            if (itemIndex == 4)
            {
                int extraStamina = PlayerPrefs.GetInt("ExtraStamina", 0);
                int maxStamina = 3 + extraStamina;

                int currentStamina = PlayerPrefs.GetInt(PlayerPrefsKeys.currentStaminaKey, maxStamina);

                if (currentStamina >= maxStamina)
                {
                    Debug.Log("Stamina llena, no podés comprar este ítem.");
                    return;
                }

                currentStamina++;
                PlayerPrefs.SetInt(PlayerPrefsKeys.currentStaminaKey, currentStamina);
                PlayerPrefs.Save();

                if (staminaSystem != null)
                    staminaSystem.ForceRefreshUI();
            }

            // STAMINA A 5/5 (ITEM 6)
            if (itemIndex == 5)
            {
                if (PlayerPrefs.GetInt("ITEM5_Comprado", 0) == 1)
                {
                    Debug.Log("El ítem 5 ya fue comprado. No se puede repetir.");
                    return;
                }

                PlayerPrefs.SetInt("ITEM5_Comprado", 1);

                int extraStamina = 2;
                PlayerPrefs.SetInt("ExtraStamina", extraStamina);

                int newMax = 3 + extraStamina;

                PlayerPrefs.SetInt(PlayerPrefsKeys.currentStaminaKey, newMax);
                PlayerPrefs.Save();

                if (staminaSystem != null)
                    staminaSystem.ForceRefreshUI();
            }

            // MÚSICA ( SOLO SE EJECUTA SI REALMENTE FUE COMPRADO)
            if (item.equipable)
            {
                AudioShop.Instance.PlayMusic(item.clipIndex);
                PlayerPrefs.SetInt("SelectedMusic", item.clipIndex);
            }

            PlayerPrefs.Save();
            ActualizarMonedasUI();

            Debug.Log("Compraste: " + item.itemNombre);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }

    private void AplicarEstiloComprado(GameObject slot)
    {
        // OSCURECER EL FONDO 
        Image bg = slot.GetComponent<Image>();
        if (bg != null)
            bg.color = new Color(0.75f, 0.75f, 0.75f, 1f);


        Image icono = slot.transform.Find("Icono").GetComponent<Image>();
        if (icono != null)
            icono.color = new Color(0.2f, 0.2f, 0.2f, 1f);

        // OPCIONAL: texto "Comprado" (solo si querés)
        Transform texto = slot.transform.Find("Texto");
        if (texto != null)
        {
            TMP_Text txt = texto.GetComponent<TMP_Text>();
            if (txt != null)
                txt.text = "Comprado";
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

