using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Destroyer OnTriggerEnter");
        //여기에 닿은 오브젝트가 Ingredient 태그를 가지고 있으면
        if (other.gameObject.tag.Contains("Ingredient"))
        {
            //오브젝트를 파괴
            Delete_Object(other.gameObject);
        }
    }

    private void Delete_Object(GameObject g_obj){
        //null이 아니고 자식 오브젝트가 있으면
        if(g_obj != null) {
            if(g_obj.transform.childCount > 0){
                Delete_Object(g_obj.transform.GetChild(0).gameObject);
            }
            GameObject.Destroy(g_obj);
        }
    }
}
