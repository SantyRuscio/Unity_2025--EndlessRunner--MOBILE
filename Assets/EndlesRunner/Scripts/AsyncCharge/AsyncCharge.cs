using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;

public class AsyncCharge : MonoBehaviour
{
    //[SerializeField] int sceneName;
    [SerializeField] Image _loader;
    [SerializeField] GameObject _panel;
    AsyncOperation _operation;

    public void StartLevel(string sceneName)
    {
        StartCoroutine(StartChargingTheScene(sceneName));
    }

    IEnumerator StartChargingTheScene(string sceneName)
    {
        _loader.fillAmount = 0;
        _panel.SetActive(true);

        _operation = SceneManager.LoadSceneAsync(sceneName); // LoadSceneMode.Single automático

        _operation.allowSceneActivation = false;


        //extra:
       // Application.backgroundLoadingPriority = ThreadPriority.Low;
         Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
        //  Application.backgroundLoadingPriority = ThreadPriority.Normal;
        //  Application.backgroundLoadingPriority = ThreadPriority.High;


        while (!_operation.isDone)
        {
            float progress = Mathf.Clamp01(_operation.progress / 0.9f);
            _loader.fillAmount = Mathf.MoveTowards(_loader.fillAmount, progress, Time.deltaTime);

            // Cuando la barra llega a 1, activamos la escena
            if (_loader.fillAmount >= 1f)
            {
                _operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

    private void OnEnable()
    {
        if (_panel != null)
            _panel.SetActive(false);

        if (_loader != null)
            _loader.fillAmount = 0;

        _operation = null;
    }


}