using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Android : MonoBehaviour
{
    private float HorizontalInput;
    float VerticalInput;
    bool HandBrake;
    
    public float _HorizontalInput
    {
        set
        {
                HorizontalInput = value;
        }
        get => HorizontalInput;
    }
    public float _VerticalInput
    {
        set
        {
            VerticalInput = value;
        }
        get => VerticalInput;
    }
    public bool _HandBrake
    {
        set
        {
            HandBrake = value;
            if (HandBrake) _VerticalInput = 1f;
            else _VerticalInput = 0f;
        }
        get => HandBrake;
    }
}
