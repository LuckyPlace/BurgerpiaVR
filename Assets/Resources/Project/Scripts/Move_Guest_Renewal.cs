using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;


public class Move_Guest_Renewal : MonoBehaviour
{
    GameManager gm;
    public List<GameObject> waypoints;
    public List<GameObject> counters;
    public float MoveSpeed = 3.0f;
    public float RotationSpeed = 45.0f;
    public float RotationAngle = -90.0f;
    //최종 목적지와 손님 퇴장지점
    public GameObject last_target = null;
    public GameObject guest_out;

    //주문 결과에 따른 손님 색상 변경을 위한 마테리얼
    public Material this_material;
    Material guest_color;
    //마테리얼을 적용받을 손님 데이터
    public GameObject guest;
    //index
    public int index = 0;

    void Start()
    {
        //단일 객체 색상 변경을 위한 마테리얼 복사 적용
        guest_color = new Material(this_material);
        guest.GetComponent<Renderer>().material = guest_color;
        gm = GameManager.pub_ins;
        waypoints = new List<GameObject>(gm.waypoints);
        counters = gm.counters;
        //카운터 + 대기실 자리 중 빈 자리를 찾기
        Find_Empty();
        //빈 자리가 아예 없다면 손님이 즉시 퇴장하며, 라이프 차감
        if(last_target == guest_out){
            waypoints.Add(guest_out);
            guest_color.color = Color.red;
            StartCoroutine(Go_Outside_Coroutine());
        }
        //경유지에 last target 넣기
        else{
            waypoints.Add(last_target);
            StartCoroutine(Move_Customer(waypoints[index]));
        }
    }

    //손님 퇴장 코루틴
    IEnumerator Go_Outside_Coroutine(){

        //경유지 배열을 퇴장용으로 교체
        waypoints.Clear();
        waypoints = gm.waypoints_out;
        //퇴장 경유지로 이동
        index = 0;
        Debug.Log("Go_Out_Customer index : " + index);
        StartCoroutine(Move_Customer(waypoints[index]));
        //주문을 실패했거나 바로 나가는 경우 목숨 차감 실행
        if(guest_color.color == Color.red) gm.Guest_Do_Life_Minus();
        yield return null;
        
        /* 기존 사용 코드
        index = waypoints.Count - 1;
        while(Vector3.Distance(transform.position, waypoints[index-1].transform.position) > 0.01f){
            transform.position = Vector3.MoveTowards(transform.position, waypoints[index-1].transform.position
                                                    , MoveSpeed * Time.deltaTime);
            yield return null;
        }
        while(Vector3.Distance(transform.position, guest_out.transform.position) > 0.01f){
            transform.position = Vector3.MoveTowards(transform.position, guest_out.transform.position
                                                    , MoveSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
        UnityEngine.Object.Destroy(gameObject);*/
    }

    IEnumerator Move_Customer(GameObject waypoint)
    {
        Vector3 wp_position = waypoint.transform.position;
        Vector3 next_waypoint = new Vector3(wp_position.x, transform.position.y, wp_position.z);

        while (Vector3.Distance(transform.position, next_waypoint) > 0.01f)
        {
            //다음 이동 지점으로 이동
            transform.position = Vector3.MoveTowards(transform.position, next_waypoint, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        //도착을 했다면 잠시 기다렸다가 재귀적 호출
        yield return new WaitForSeconds(1.0f);
        if(Check_Distance(waypoint.transform)){
            if(index < waypoints.Count - 1) index++;
            StartCoroutine(Move_Customer(waypoints[index]));
        }
    }

    //회전 코루틴
    IEnumerator Rotation_Coroutine() {
        Quaternion target = Quaternion.Euler(0, 0, 0);
        while(transform.rotation != target){
            //각도가 같아질 때까지 회전
            transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, target, RotationSpeed * Time.deltaTime);
        }
        yield return null;
    }

    //빈 자리 찾기
    void Find_Empty(){
        for(int i = 0; i < counters.Count; i++){
            //빈 카운터 자리 찾기
            if(counters[i].GetComponent<OrderWP_flag>().flag == false){
                last_target = counters[i];
                counters[i].GetComponent<OrderWP_flag>().flag = true;
                return;
            }
        }
        last_target = guest_out;
    }

    //거리 측정용
    bool Check_Distance(Transform waypoint){
        if(Mathf.Abs(waypoint.position.x - transform.position.x) <= 0.01f 
            && Mathf.Abs(waypoint.position.z - transform.position.z) <= 0.01f)
        return true;
        else return false;
    }

    //Trigger 입장 시 태그 확인 및 회전
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Waypoint_Door")){
            StartCoroutine(Rotation_Coroutine());
        }
        //손님의 퇴장 구현
        if(other.CompareTag("Waypoint_Out")){
            gameObject.SetActive(false);
            UnityEngine.Object.Destroy(gameObject);
        }
    }
    
    //손님 퇴장하기
    public void Go_Outside(){
        StartCoroutine(Go_Outside_Coroutine());
    }

    //주문 성공/실패에 따라 손님의 색을 다르게 표현
    public void Red_or_Green(bool order_result){
        //주문 결과에 따라 손님 색상 변경
        if(order_result){
            guest_color.color = Color.green;
        } else {
            guest_color.color = Color.red;
        }
    }
}