using UnityEngine;
using System.Collections;
using Ing_Enum;

public class Duplicate_Ingredient : MonoBehaviour
{
    //복제할 재료
    public GameObject ingredient;
    public Ing_List ing_tag;
    //오브젝트 생성을 기다릴 시간
    float waitTime = 1.0f;
    //충돌이 존재하지 않는 시간
    float idleTime = 0.0f;
    //코루틴 변수
    IEnumerator dupCoroutine;

    IEnumerator Do_Duplicate()
    {
        //일정 시간만큼 기다리기
        yield return new WaitForSeconds(waitTime);
        //충돌 없으면 복제
        if(Time.time - idleTime > waitTime){
            GameObject dup = Instantiate(ingredient, transform.position, Quaternion.identity);
            dup.tag = "Ingredient";
        }
    }

    //대상 위치에 일정 시간 이상 충돌하고 있는 물체가 없다면 + 해당 Duplicator에 할당된 재료라면 복제본을 생성
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag.Contains("Ingredient") && collision.gameObject.GetComponent<Ing_Code>().ing == ing_tag){
            //idleTime을 현재 시간으로 설정
            idleTime = Time.time;
            //waitTime만큼 대기한 후 복제를 실시하는 코루틴 실행
            dupCoroutine = Do_Duplicate();
            StartCoroutine(dupCoroutine);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Contains("Ingredient")){
            //충돌이 발생하면 idleTime을 초기화
            idleTime = 0.0f;
            //코루틴을 중지하여 불필요한 복제 생성 방지
            if(dupCoroutine != null) StopCoroutine(dupCoroutine);
        }
    }
}
