using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject MainCamera;
    [SerializeField] EventTrigger ResetButton;
    [SerializeField] EventTrigger Gas, Break, HandBrake, Left, Right;

    public Car currentCar;
    GameObject car;

    Transform cameraPosition;
    public GameObject _car
    {
        get => car;
    }    // Start is called before the first frame update
    private void Awake()
    {
        currentCar = GameObject.Find("PlayerProgress").GetComponent<PlayerProgress>().currentCar;
        car = Instantiate(currentCar.carObject);
        car.transform.position = gameObject.transform.position;
        car.transform.rotation = gameObject.transform.rotation;
        car.name = "PlayerCar";

        CarComponents();
        CarUpgrades();
        CarModifications();
        SetButtons();
        Destroy(gameObject);
    }

    void SetButtons()
    {
        CarController currentCarController = currentCar.carObject.GetComponent<CarController>();

        //Gas.onClick.AddListener(() => { currentCarController._VerticalInput(1f); });
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        //entry.callback.AddListener(currentCarController)


    }
    void CarComponents()
    {
        cameraPosition = car.transform.Find("CameraPosition").transform;

        MainCamera.GetComponent<CameraFollow>().SetCameraFollowComponents(car.transform, cameraPosition); //Camera Settings
    }
    void CarUpgrades()
    {
        car.GetComponent<CarController>().AddUpgrades(currentCar.currentMaxSpeedLevel, currentCar.currentAccelerationLevel, currentCar.currentDriftLevel, currentCar.currentTest4uLevel);
    }
    void CarModifications()
    {
        GameObject.Find("Body").GetComponent<MeshRenderer>().material.color = currentCar.currentColor;
    }
}
