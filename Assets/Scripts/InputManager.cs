using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float MainHorizontal()
    {
        float r = 0.0f;
            r += Input.GetAxis("Horizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float MainVertical()
    {
        float r = 0.0f;
            r += Input.GetAxis("Vertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static bool HandBrakeDown()
    {
        return Input.GetButtonDown("HandBrake");
        
    }
    public static bool HandBrakeUp()
    {
        return Input.GetButtonUp("HandBrake");
    }
    public static bool Gas()
    {
        return Input.GetButtonDown("Gas");
    }
    public static bool Brake()
    {
        return Input.GetButtonDown("Brake");
    }
}
