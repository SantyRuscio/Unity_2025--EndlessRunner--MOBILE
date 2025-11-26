using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpSlot : MonoBehaviour
{
    [SerializeField] private Image powerUpImage;
    [SerializeField] private float blinkTime = 2f;
    [SerializeField] private float blinkInterval = 0.15f;

    private Coroutine activeRoutine;

    private void Start()
    {
        EventManager.Subscribe(TypeEvents.PowerUpImageSlot, SetActiveUI);

        EventManager.Subscribe(TypeEvents.GameOver, SetInActiveUI);

        // Empezar invisible
        powerUpImage.enabled = false;
    }

    private void SetActiveUI(object[] parameters)
    {
        if (parameters.Length > 0 && parameters[0] is Sprite sprite)
        {
            powerUpImage.sprite = sprite;
            powerUpImage.enabled = true;

            if (activeRoutine != null)
                StopCoroutine(activeRoutine);

            activeRoutine = StartCoroutine(ShowPowerUpRoutine());
        }
    }

    private IEnumerator ShowPowerUpRoutine()
    {
        float timer = 0f;
        bool blink = false;

        while (timer < GameManager.PowerUpDuration)
        {
            // Titileo solo en los últimos segundos
            if (timer >= GameManager.PowerUpDuration - blinkTime)
            {
                blink = !blink;
                powerUpImage.enabled = blink;
                yield return new WaitForSeconds(blinkInterval);
                timer += blinkInterval;
            }
            else
            {
                powerUpImage.enabled = true;
                yield return null;
                timer += Time.deltaTime;
            }
        }

        // Apagar al final
        powerUpImage.enabled = false;
        powerUpImage.sprite = null;
    }

    private void SetInActiveUI(object[] parameters)
    {
        powerUpImage.enabled = false;
        powerUpImage.sprite = null;
    }
    private void OnDestroy()
    {
        EventManager.Unsubscribe(TypeEvents.PowerUpImageSlot, SetActiveUI);

        EventManager.Unsubscribe(TypeEvents.GameOver, SetInActiveUI);
    }
}

