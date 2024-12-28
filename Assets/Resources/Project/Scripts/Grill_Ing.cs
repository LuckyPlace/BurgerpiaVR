using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ing_Enum;

public class Grill_Ing : MonoBehaviour
{
    Dictionary<GameObject, GameObject> grillable = new Dictionary<GameObject, GameObject>();
    public List<GameObject> grillable_ing = new List<GameObject>();
    public GameObject grillEffect;
    private int grillObjectCounter = 0;

    void Start(){
        Set_Dict();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Grillable>() != null)
        {
            grillEffect.SetActive(true);
            grillObjectCounter++;
        }
    }

    private void OnCollisionStay(Collision collision)
    {   
        if(collision.gameObject.GetComponent<Grillable>() != null){
            //열거형을 이용하여 인스턴스화 대신에 Inspector를 이용하여 프리팹 가져오기
            Ing_List reference = collision.gameObject.GetComponent<Grillable>().refer;
            GameObject refer_obj = Resources.Load<GameObject>("Project/Prefab/Ingredient/" + reference.ToString());
            //그릴 위에 올라간 재료가 구울 수 있는 재료일 때 시간을 더하기
            if(grillable.ContainsKey(refer_obj)) collision.gameObject.GetComponent<Grillable>().collisionTime += Time.deltaTime;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (grillObjectCounter > 0)
            grillObjectCounter--;
        if(grillObjectCounter == 0)
        {
            grillEffect.SetActive(false);
        }
    }

    void Set_Dict(){
        //그릴 가능한 재료들을 딕셔너리에 저장
        for(int i = 0; i < grillable_ing.Count; i++){
            grillable.Add(grillable_ing[i], grillable_ing[i]);
        }
    }
}