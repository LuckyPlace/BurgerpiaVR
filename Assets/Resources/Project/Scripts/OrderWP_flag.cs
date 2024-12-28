using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderWP_flag : MonoBehaviour
{
    //해당 웨이포인트에 다른 손님이 존재하는 지 판별하는 용도
    public bool flag = false;
    //해당 웨이포인트에 존재하는 손님 (카운터 한정)
    public GameObject guest = null;
    //손님 정보 전달용 (카운터 한정)
    public GameObject server;
    //손님이 온다면 연결된 메뉴판에 메뉴를 출력
    public GameObject menu;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Guest" && this.gameObject.tag == "Waypoint_Counter"){
            flag = true;
            guest = other.gameObject;
            server.GetComponent<Serve_Menu>().GetGuest();
            menu.GetComponent<Show_Menu>().Display_Menu(guest);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Guest"){
            flag = false;
        }
        //카운터에서 손님이 나가면 메뉴판 지우기
        if(other.gameObject.tag == "Guest" && gameObject.tag == "Waypoint_Counter"){
            menu.GetComponent<Show_Menu>().Discard_Menu();
            guest = null;
            server.GetComponent<Serve_Menu>().NullGuest();
        }
    }
}
