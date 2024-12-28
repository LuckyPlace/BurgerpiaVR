using Ing_Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucable : MonoBehaviour
{
    public GameObject saucedObject;
    public Ing_List refer;
    public bool isValid = false;

    private void Update()
    {
        if(isValid)
        {
            gameObject.SetActive(false);
            Instantiate(saucedObject, transform.position, transform.rotation);
            isValid = false;
            Destroy(gameObject);
        }
    }
}
