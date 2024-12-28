using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ing_Enum;

public class Serve_Menu : MonoBehaviour
{
    // Start is called before the first frame update

    //손님과 주문받는 접시가 맡는 카운터 자리
    public GameObject guest;
    public GameObject counter;

    public GameObject life;
    public List<Ing_List> order;

    //손님 받아오기 및 손님 초기화
    public void GetGuest(){
        //null Check
        if(counter.GetComponent<OrderWP_flag>().guest == null) return;
        guest = counter.GetComponent<OrderWP_flag>().guest;
        order = guest.GetComponent<Make_Order>().order;
    }
    
    public void NullGuest(){
        //손님이 나갔는 지 확인
        if(counter.GetComponent<OrderWP_flag>().guest != null) return;
        guest = null;
    }

    //접시에서 음식을 전달받았을 때 메뉴와 비교하고 맞다면 그냥 퇴장, 아니라면 라이프를 감소시키고 퇴장
    public bool Check_Menu(GameObject food){
        //코드 단축을 위한 변수 설정
        //우선 제일 위가 되는 부모를 받아와서 비교한다
        GameObject tmp = food;
        Debug.Log("메뉴 : " + order[0] + ", 현재 음식 : " + tmp.gameObject.GetComponent<Ing_Code>().ing);
        if(tmp.gameObject.GetComponent<Ing_Code>().ing == order[0]){
            for(int i = 1; i < order.Count; i++){
                GameObject nxting = null;
                //nxting에 다음 재료를 저장
                for(int j = 0; j < tmp.transform.childCount; j++){
                    if(tmp.transform.GetChild(j).gameObject.tag == "Ingredient"){
                        nxting = tmp.transform.GetChild(j).gameObject;
                        break;
                    }
                }
                if(nxting == null) return false;
                tmp = nxting;
                Debug.Log("메뉴 : " + order[i] + ", 현재 음식 : " + tmp.gameObject.GetComponent<Ing_Code>().ing);
                if(tmp.gameObject.GetComponent<Ing_Code>().ing != order[i]) return false;
            }  
        } //아니면 주문이 잘못된 것이므로 거짓을 반환
        else return false;

        return true;
    }

    //서빙용 접시에 음식이 올라갔다면 메뉴가 맞는 지 확인
    public void Do_Check(GameObject food){
        //null을 피하기 위한 초기화
        bool result = false;
        //null check : 손님이 없을 때 서빙 접시에 음식을 놓으면 음식을 삭제한다
        if(guest == null) {
            Destroy(food);
            return;
        }
        if(food.tag == "Ingredient") 
            result = Check_Menu(food);
        //메뉴가 맞다면 손님 퇴장, 메뉴 치우기는 다른 스크립트에서 처리
        if(result){
            Debug.Log("Order Complete");
            guest.GetComponent<Move_Guest_Renewal>().Red_or_Green(result);
            guest.GetComponent<Move_Guest_Renewal>().Go_Outside();
            Destroy(food);
        } else {
            //목숨 차감
            Debug.Log("Order Failed");
            guest.GetComponent<Move_Guest_Renewal>().Red_or_Green(result);
            guest.GetComponent<Move_Guest_Renewal>().Go_Outside();
            Destroy(food);
        }
    }  
}
