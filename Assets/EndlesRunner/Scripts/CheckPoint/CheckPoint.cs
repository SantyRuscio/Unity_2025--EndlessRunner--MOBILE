using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("CheckPointGuarda");
            GameManager.instance.SaveMethod();
        }
    }
}

