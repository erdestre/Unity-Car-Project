using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CarController>() != null)
        {
            gameObject.transform.parent.GetComponent<CheckPointRace>().Next();
        }
    }
}
