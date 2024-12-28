using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using Ing_Enum;

public class Show_Menu : MonoBehaviour {
    //메뉴를 보여줄 종합 화면
    public GameObject display;
    //메뉴 구성이 들어갈 자리와 메뉴에 둘어올 수 있는 재료의 최대 개수
    static int menu_num = 10;
    public GameObject[] menu_arr = new GameObject[menu_num];

    //메뉴를 가지고 올 손님과 손님의 리스트
    public List<Ing_List> menu_list = new List<Ing_List>();

    //메뉴판이 보여줄 손님이 위치할 자리
    public GameObject waypoint;

    //재료 프리팹을 저장할 리스트
    public List<GameObject> ing_list = new List<GameObject>();

    //현재 메뉴를 표시해야 하는 지를 결정하는 플래그
    public bool flag = true;

    //게임 매니저에 저장된 재료 리스트 가져오기
    GameManager gm;

    void Start() {
        gm = GameManager.pub_ins;
        ing_list = gm.ing_list;
    }

    //메뉴를 화면에 표시하는 메서드
    public void Display_Menu(GameObject guest){
        Debug.Log("Menu Display On");
        //기존 메뉴 리스트를 초기화 하고 손님의 메뉴 리스트를 가져옴
        menu_list.Clear();
        menu_list = guest.GetComponent<Make_Order>().order;

        //메뉴 리스트를 순회하며 메뉴를 화면에 표시
        for(int i = 0; i < menu_list.Count; i++){
            //열거형에 저장된 재료를 가져오기
            Ing_List ing = (Ing_List)Enum.Parse(typeof(Ing_List), menu_list[i].ToString());
            GameObject ingredient = ing_list[(int)ing];
            //메뉴를 화면에 표시
            GameObject newIngredient = Instantiate(ingredient, menu_arr[i].transform.position, Quaternion.identity);
            //메뉴가 움직이지 않도록 설정
            newIngredient.GetComponent<XRGrabInteractable>().enabled = false;
            newIngredient.GetComponent<Rigidbody>().isKinematic = true;
            newIngredient.GetComponent<Stacker>().enabled = false;
            //메뉴를 회전하고 중앙에 배치
            newIngredient.transform.Rotate(90, 0, 0);
            newIngredient.transform.position = menu_arr[i].transform.position+new Vector3(0, 0.1f, 0.1f);
            //각 메뉴를 부모로 할당
            newIngredient.transform.parent = menu_arr[i].transform;
        }
    }

    public void Discard_Menu(){
        //메뉴판 지우기
        for(int i = 0; i < menu_list.Count; i++){
            Destroy(menu_arr[i].transform.GetChild(0).gameObject);
        }
    }
}