using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ing_Enum;

public class Grillable : MonoBehaviour
{
    public GameObject cookedObject;
    public Ing_List refer;
    public float collisionTime = 0f;

    private void Update()
    {
        if(collisionTime >= 5f)
        {
            gameObject.SetActive(false);
            Instantiate(cookedObject, transform.position, transform.rotation);
            collisionTime = 0f;
            Destroy(gameObject);
        }
    }
}
