using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Action OnJump;
    public Action<float> OnMove;
    public Action OnRoll;


    // Update is called once per frame
    void Update()
    {
        OnMove(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnRoll();
        }

    }
}