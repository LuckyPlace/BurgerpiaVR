using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauce_Bottle_Respawn : MonoBehaviour
{
    private Vector3 originPos;
    void Start()
    {
        originPos = transform.position;
    }

    void Update()
    {
        if(transform.position.x < -0.6f + originPos.x || transform.position.x > 2.7f + originPos.x || transform.position.z < -1.2f + originPos.z || transform.position.z > 3.6f + originPos.z)
        {
            Debug.Log("x: " + transform.position.x + ", z: " + transform.position.z);
            transform.position = originPos;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
