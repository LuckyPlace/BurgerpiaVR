using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_CheckMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ingredient")
        {
            parent.GetComponent<Serve_Menu>().Do_Check(collision.gameObject);
        }
    }
}
