using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ing_Enum;

public class Cutting_Foods : MonoBehaviour
{
    public GameObject slicedCheese;         //Slice cheese
    public GameObject slicedFish;           //Slice fish

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ingredient"){
            if(collision.gameObject.GetComponent<Ing_Code>().ing.ToString() == "Cheese")     //큰 치즈와 부딪히면
            {
                collision.gameObject.SetActive(false);              //큰 치즈 비활성화 후 작은 치즈 생성
                GameObject cheese = Instantiate(slicedCheese, collision.transform.position, Quaternion.identity);
                GameObject.Destroy(collision.gameObject); //파괴
            }
            else if(collision.gameObject.GetComponent<Ing_Code>().ing.ToString() == "Fish")              //생선과 부딪히면
            {
                collision.gameObject.SetActive(false);              //생선 비활성화
                GameObject fish = Instantiate(slicedFish, collision.transform.position, Quaternion.identity);       //슬라이스 생선 생성
                GameObject.Destroy(collision.gameObject); //파괴
            }
        }

    }
}