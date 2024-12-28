using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    public GameObject Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Guest")
        {
            Door.GetComponent<DoorOpener>().open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Guest")
        {
            Door.GetComponent<DoorOpener>().open = false;
        }
    }
}
