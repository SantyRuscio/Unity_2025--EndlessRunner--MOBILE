using UnityEngine;

public class WinExecuter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            Debug.Log("PLAYER DENTRO");
            EventManager.Trigger(TypeEcvents.Win);
        }
    }
}