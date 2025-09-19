using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Action OnJump;
    public Action OnTouchAndClick;
    public Action<float> OnMove;
    public Action OnRoll;


    [Header("Aceler�metro (solo m�vil)")]
    [SerializeField] private float tiltSensitivity = 2f; //ESTA SENSIBILIDAD LA USO PARA DETECTAR EL MOVIMIENTO

    [Header("Swipe")]
    [SerializeField] private float minSwipeDistance = 100f;
    private Vector2 startTouch;
    private bool swipeDetected;


    void Update()
    {

#if UNITY_EDITOR //SIMULO SWIPE DESDE PC
        if (Input.GetMouseButtonDown(0)) startTouch = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 swipeDelta = (Vector2)Input.mousePosition - startTouch;
            if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
            {
                if (swipeDelta.y > 0) OnJump?.Invoke();
                else OnRoll?.Invoke();
            }
        }
#endif

#if UNITY_ANDROID || UNITY_IOS //MOBILE

        if (!SystemInfo.supportsAccelerometer)
        {
            Debug.Log("NO LO SOPORTA");
           // return;
        }
        else
        {
            OnMove(Input.acceleration.x * tiltSensitivity); 
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                startTouch = touch.position;

            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeDelta = touch.position - startTouch;
                DetectSwipe(swipeDelta);
            }
        }
#endif

#if UNITY_EDITOR || UNITY_STANDALONE //PC
        
        OnMove(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnRoll();
        }
#endif

#if UNITY_EDITOR || UNITY_STANDALONE //PC
        if (Input.GetMouseButtonDown(0))
        {
            OnTouchAndClick();
        }
#endif

        
#if UNITY_ANDROID || UNITY_IOS //MOBILE
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTouchAndClick();
        }
#endif
    }
    private void DetectSwipe(Vector2 swipeDelta)
    {
        if (swipeDelta.magnitude < minSwipeDistance)
            return;

        if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
        {
            if (swipeDelta.y > 0)
            {
                if (OnJump != null)
                    OnJump.Invoke(); // Swipe salto
            }
            else
            {
                if (OnRoll != null)
                    OnRoll.Invoke(); // Swipe Roll
            }
        }
        

    }
}