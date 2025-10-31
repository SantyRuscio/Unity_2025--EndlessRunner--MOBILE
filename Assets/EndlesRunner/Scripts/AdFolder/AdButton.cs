using UnityEngine;

public class AdButton : MonoBehaviour
{
    public void ExecuteButton() => AdsManager.Instance.ShowRewardedAd();
}
