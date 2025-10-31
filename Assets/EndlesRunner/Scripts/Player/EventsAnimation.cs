using UnityEngine;

public class EventsAnimation : MonoBehaviour
{
    [SerializeField] PlayerModel PlayerModel;
    public void OnRollEnd()
    {
        PlayerModel.OnRollEnd();
    }
}
