using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string playerTag = "Player";

    public abstract void Execute();

    public virtual void Initialize(ItemsFactory factory)
    {
        gameObject.SetActive(true);
    }

    public virtual void ResetObject()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Execute();
        }
    }
}

