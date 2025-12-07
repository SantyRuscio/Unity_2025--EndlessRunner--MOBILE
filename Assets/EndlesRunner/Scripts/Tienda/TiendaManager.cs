using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TiendaManager : MonoBehaviour, IScreen
{
    [Header("UI Tienda")]
    public GameObject tiendaPanel;
    public TMP_Text monedasTexto;  // ← MONEDAS VISIBLES EN LA TIENDA
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

            int itemIndex = Array.IndexOf(items, item);

            //SUMAR STAMINA DE A 1 
            if (itemIndex == 3)
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

                Debug.Log("Sumaste +1 stamina. Ahora tenés: " + currentStamina);

                if (staminaSystem != null)
                    staminaSystem.ForceRefreshUI();
            }

            // STAMINA A 5/5 (SOLO UNA VEZ)
            if (itemIndex == 4)
            {
                // Ya comprado ,no permitir repetir
                if (PlayerPrefs.GetInt("ITEM5_Comprado", 0) == 1)
                {
                    Debug.Log("El ítem 5 ya fue comprado. No se puede repetir.");
                    return;
                }

                // Marcar como comprado
                PlayerPrefs.SetInt("ITEM5_Comprado", 1);

                // Upgrade del máximo
                int extraStamina = 2; // 3 base + 2 = 5 máximo total
                PlayerPrefs.SetInt("ExtraStamina", extraStamina);

                int newMax = 3 + extraStamina;

                // Llenar stamina actual a 5/5
                PlayerPrefs.SetInt(PlayerPrefsKeys.currentStaminaKey, newMax);
                PlayerPrefs.Save();

                Debug.Log("Upgrade comprado: nuevo máximo = " + newMax);

                // Refrescar UI
                if (staminaSystem != null)
                    staminaSystem.ForceRefreshUI();
            }

            // Si el ítem es equipable
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

