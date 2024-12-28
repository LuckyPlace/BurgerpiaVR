using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife_Cage : MonoBehaviour
{
    public GameObject knife;
    public GameObject plate;

    bool flag = false;

    void Update(){
        if(flag){
            knife.transform.position = plate.transform.position + new Vector3(0, 0.1f, 0);
            knife.transform.rotation = new Quaternion(0, 0, 0, 0);
            knife.GetComponent<Rigidbody>().velocity = Vector3.zero;   
            flag = false;   
        }
    //칼이 바닥을 뚫었을 때
        if(knife.transform.position.y < 0f){
            knife.transform.position = plate.transform.position + new Vector3(0, 0.1f, 0);
            knife.transform.rotation = new Quaternion(0, 0, 0, 0);
            knife.GetComponent<Rigidbody>().velocity = Vector3.zero;   
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject == knife){
            flag = true;
        }
    }
}
