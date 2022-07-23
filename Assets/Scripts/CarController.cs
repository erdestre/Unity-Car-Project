using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //left, right, gas, brake, handbrake
    //SpinWheel, SteerWheel, Accelerate
    [Header("Car Features")]
    [SerializeField] bool isFWD, isBWD;
    [SerializeField] float MotorPower;
    [SerializeField] float WheelSteer;
    [SerializeField] float HandBrakeTorque = 600f;
    [SerializeField] float DefaultExtremumSlip = 0.3f;
    [SerializeField] float HandBrakeExtremumSlip = 2f;
    [SerializeField] TrailRenderer[] tireMarks;

    [Header("Car Mechanics")]
    public WheelCollider CRightFront, CLeftFront;
    [SerializeField] WheelCollider CRightRear, CLeftRear;
    [SerializeField] Transform TRightFront, TLeftFront, TRightRear, TLeftRear;

    Transform CheckPoint;
    GameObject AndroidCanvas;
    Android android;

    public Transform _CheckPoint
    {
        set
        {
            CheckPoint = value;
        }
    }

    float magnitude;
    public float _magnitude
    {
        get => magnitude;
    }

    bool isDrifting;
    public bool _isDrifting
    {
        get => isDrifting;
    }


    private void Start()
    {
        LoadCar();
        AndroidCanvas = GameObject.Find("AndroidCanvas");
        android = AndroidCanvas.GetComponent<Android>();
    }

    private void LoadCar()
    {

    }

    private void GetInputs()
    {
        
        if (Application.isEditor)
        {
            android._HorizontalInput = InputManager.MainHorizontal();
            android._VerticalInput = InputManager.MainVertical();
            if (InputManager.HandBrakeDown())
            {
                android._HandBrake = true;
            }
            if (InputManager.HandBrakeUp())
            {
                android._HandBrake = false;
            }
        }
        
    }
    private void Steer()
    {
        CLeftFront.steerAngle = android._HorizontalInput * WheelSteer;
        CRightFront.steerAngle = android._HorizontalInput * WheelSteer;
    }
    private void Accelerate()
    {
        CLeftFront.motorTorque = android._VerticalInput * MotorPower;
        CRightFront.motorTorque = android._VerticalInput * MotorPower;
        CRightRear.motorTorque = android._VerticalInput * MotorPower;
        CLeftRear.motorTorque = android._VerticalInput * MotorPower;

    }
    private void WheelAnimation()
    {
        ChangeWheelPose(CLeftFront, TLeftFront);
        ChangeWheelPose(CLeftRear, TLeftRear);
        ChangeWheelPose(CRightFront, TRightFront);
        ChangeWheelPose(CRightRear, TRightRear);
    }

    private void ChangeWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 wheelPosition = wheelCollider.transform.position;
        Quaternion wheelRotation = wheelCollider.transform.rotation;

        wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);

        wheelTransform.position = wheelPosition;
        wheelTransform.rotation = wheelRotation;
    }
    private void Update()
    {
        GetInputs();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Steer();
        Accelerate();
        WheelAnimation();
        ActivateHandBrake();

        CarBehaviour();
        CheckDrift();
    }
    private void CheckDrift()
    {
        float m_speedAngle = Vector3.Angle(gameObject.GetComponent<Rigidbody>().velocity, gameObject.transform.forward) * Mathf.Sign(Vector3.Dot(gameObject.GetComponent<Rigidbody>().velocity, gameObject.transform.right));
        magnitude = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        if (magnitude > 18 && (m_speedAngle > 7 || m_speedAngle < -7))
        {
            isDrifting = true;
            StartEmmitter();
        }
        else
        {
            StopEmitter();
            isDrifting = false;
        }
    }


    private void StopEmitter()
    {
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = false;
        }
    }

    private void StartEmmitter()
    {
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = true;
        }
    }

    private void CarBehaviour()
    {
        if (android._HandBrake)
        {
            WheelFrictionCurve myWfc;
            myWfc = CLeftFront.sidewaysFriction;
            myWfc.extremumSlip = HandBrakeExtremumSlip;
            myWfc.stiffness = 2.5f;

            CLeftFront.sidewaysFriction = myWfc;
            CRightFront.sidewaysFriction = myWfc;

            myWfc.stiffness = myWfc.stiffness / 2;
            CLeftRear.sidewaysFriction = myWfc;
            CRightRear.sidewaysFriction = myWfc;

        }
        else
        {
            WheelFrictionCurve myWfc;
            myWfc = CLeftFront.sidewaysFriction;
            myWfc.extremumSlip = DefaultExtremumSlip;
            myWfc.stiffness = 1f;

            CLeftFront.sidewaysFriction = myWfc;
            CLeftRear.sidewaysFriction = myWfc;
            CRightFront.sidewaysFriction = myWfc;
            CRightRear.sidewaysFriction = myWfc;
        }
    }
    private void ActivateHandBrake()
    {
        if (android._HandBrake)
        {
            //CLeftFront.brakeTorque = HandBrakeTorque;
            //CRightFront.brakeTorque = HandBrakeTorque;
            CLeftRear.brakeTorque = HandBrakeTorque;
            CRightRear.brakeTorque = HandBrakeTorque;

            //CLeftFront.motorTorque = 0f;
            //CRightFront.motorTorque = 0f;
            CLeftRear.motorTorque = 0f;
            CRightRear.motorTorque = 0f;
        }
        else
        {
            //CLeftFront.brakeTorque = 0f;
            //CRightFront.brakeTorque = 0f;
            CLeftRear.brakeTorque = 0f;
            CRightRear.brakeTorque = 0f;
        }
    }

    public void AddUpgrades(int MotorPower, int Accelerate, int drift, int test)
    {
        this.MotorPower += MotorPower*2000;
        //this.Accelerate += Accelerate;
        HandBrakeExtremumSlip += drift / 10;
        Debug.Log("Motor Power: " + MotorPower);
    }
}
