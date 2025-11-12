using System;
using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Action OnJump;
    public Action OnTouchAndClick;
    public Action<float> OnMove;
    public Action OnRoll;

    [Header("Acelerómetro (solo móvil)")]
    [SerializeField] private float tiltSensitivity = 1f;

    [Header("Swipe")]
    [SerializeField] private float minSwipeDistance = 100f;
    private Vector2 startTouch;

    private void Start()
    {
        EventManager.Subscribe(TypeEvents.GameOver, StopController);

        EventManager.Subscribe(TypeEvents.RewindEvent, RewindControll);
    }

    void Update()
    {
#if UNITY_EDITOR // SIMULO SWIPE DESDE PC
       // if (Input.GetMouseButtonDown(0)) startTouch = Input.mousePosition;
       // if (Input.GetMouseButtonUp(0))
       // {
       //     Vector2 swipeDelta = (Vector2)Input.mousePosition - startTouch;
       //     if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
       //     {
       //         if (swipeDelta.y > 0) OnJump?.Invoke();
       //         else OnRoll?.Invoke();
       //     }
       // }
#endif

#if UNITY_ANDROID || UNITY_IOS // MÓVIL
        if (SystemInfo.supportsAccelerometer)
            OnMove(Input.acceleration.x * tiltSensitivity);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                startTouch = touch.position;

            if (touch.phase == TouchPhase.Ended)
                DetectSwipe(touch.position - startTouch);
        }
#endif

#if UNITY_EDITOR || UNITY_STANDALONE // PC
        OnMove(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space)) OnJump?.Invoke();
        if (Input.GetKeyDown(KeyCode.LeftControl)) OnRoll?.Invoke();

        if (Input.GetMouseButtonDown(0)) OnTouchAndClick?.Invoke();
#endif

#if UNITY_ANDROID || UNITY_IOS // MOBILE
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            OnTouchAndClick?.Invoke();
#endif
    }

    private void DetectSwipe(Vector2 swipeDelta)
    {
        if (swipeDelta.magnitude < minSwipeDistance) return;

        if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
        {
            if (swipeDelta.y > 0) OnJump?.Invoke();
            else OnRoll?.Invoke();
        }
    }

    private void StopController(params object[] parameters)
    {
        this.enabled = false;
    }

    private void RewindControll(params object[] parameters)
    {
        StartCoroutine(TimeToActivateControls());
    }

    private IEnumerator TimeToActivateControls()
    {
        yield return new WaitForSeconds(6f);
        this.enabled = true;
        Debug.Log("1 segundos después");
    }

    private void OnDestroy()
    {
        // IMPORTANTE: desuscribirse
        EventManager.Unsubscribe(TypeEvents.GameOver, StopController);

        EventManager.Unsubscribe(TypeEvents.RewindEvent, RewindControll);
    }
}