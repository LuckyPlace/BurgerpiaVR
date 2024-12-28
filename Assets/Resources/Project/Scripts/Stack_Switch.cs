using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack_Switch : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ingredient" && other.GetComponent<Stacker>() != null){
            Stacker stk = other.GetComponent<Stacker>();
            //찬장 내에서 재료가 쌓이는 것을 방지
            stk.flag = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ingredient" && other.GetComponent<Stacker>() != null){
            other.GetComponent<Stacker>().flag = true;
        }
    }
}
