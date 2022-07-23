using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform FollowObject;
    Transform CameraPosition;
    [SerializeField] Vector3 offset;
    [SerializeField] float Speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, CameraPosition.position, Time.deltaTime * Speed);
        gameObject.transform.LookAt(FollowObject.position + offset);
    }

    public void SetCameraFollowComponents(Transform FollowObject, Transform CameraPosition)
    {
        this.FollowObject = FollowObject;
        this.CameraPosition = CameraPosition;
    }
}
