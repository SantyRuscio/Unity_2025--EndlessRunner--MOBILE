using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour, IScreen
{

    private void Start()
    {
        GetComponent<Image>().enabled = false;


    }

    public void Activate()
    {
        GetComponent<Image>().enabled = true;
    }

    public void Deactivate()
    {
        GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            ScreenManager.Instance.ActivateScreen(this);
        }
    }
}
