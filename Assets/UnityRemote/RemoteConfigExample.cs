using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEditor.ShaderGraph.Internal;

public class RemoteConfigExample : MonoBehaviour
{
    public  static RemoteConfigExample Instance { get; private set; }

    public struct userAttributes { }
    public struct appAttributes { }

    public int coinsValue;
    public float forwardSpeed;
    public float maxForwardSpeed;
    public float distanceToActivate;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    async Task InitializeRemoteConfigAsync()
    {
        await UnityServices.InitializeAsync();

        if(!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start()
    {
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());   

    }


    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instace.appConfig fetched : " + RemoteConfigService.Instance.appConfig.config.ToString());

        coinsValue = RemoteConfigService.Instance.appConfig.GetInt("CoinsValue");

        forwardSpeed = RemoteConfigService.Instance.appConfig.GetFloat("ForwardSpeed");

        maxForwardSpeed = RemoteConfigService.Instance.appConfig.GetFloat("MaxForwardSpeed");

        distanceToActivate = RemoteConfigService.Instance.appConfig.GetFloat("DistanceToActivate");
    }


}
