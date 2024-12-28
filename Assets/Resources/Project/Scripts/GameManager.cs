using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ing_Enum;
using Unity.Profiling;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager prv_ins;
    //public static으로 Getter를 구현
    public static GameManager pub_ins {
        get {
            if(prv_ins == null) {
                //Awake로 할당되었지만 null 점검을 실행
                //만약 존재하지 않는다면 새로 생성해서 자기 자신으로 할당
                GameObject gm = new GameObject("GameManager");
                prv_ins = gm.AddComponent<GameManager>();
                DontDestroyOnLoad(gm);
            }
            return prv_ins;
        }
    }
    //싱글톤에 포함될 요소들

    //손님의 이동을 위해 필요한 waypoint와 counter 배열
    public List<GameObject> waypoints;
    public List<GameObject> counters;
    //손님의 퇴장을 위한 waypoints_out 배열
    public List<GameObject> waypoints_out;

    //게임의 초기 목숨과 현재 목숨, 목숨 제어용 오브젝트
    public int life_init = 2;
    public int life_now = 2;
    public GameObject life;

    //메뉴 표시에 사용할 재료 리스트
    public List<GameObject> ing_list = new List<GameObject>();

    //게임 오버, 게임 클리어 텍스트
    public GameObject gameover_text;
    public GameObject gameclear_text;

    void Awake(){
        //접근을 위한 초기화
        if(prv_ins == null) {
            prv_ins = this;
            //다른 씬을 넘어가도 파괴되지 않도록 설정
            //싱글톤이기 때문에 전역처럼 다뤄져야 함
            DontDestroyOnLoad(gameObject);
            //게임오버, 게임 클리어도 씬 로드에 무관하게 존재해야함
            DontDestroyOnLoad(gameover_text);
            DontDestroyOnLoad(gameclear_text);
        } else {
            //이미 존재하고 있으므로 존재할 이유가 없음, 파괴
            Destroy(gameObject);
        }

        // Ing_List 열거형의 각 값에 대해
        foreach (Ing_List ing in System.Enum.GetValues(typeof(Ing_List)))
        {
            // 해당 이름의 프리팹을 로드
            GameObject prefab = Resources.Load<GameObject>("Project/Prefab/Ingredient/" + ing.ToString());

            // ingredient_list에 추가
            ing_list.Add(prefab);
        }
    }

    public void Guest_Do_Life_Minus(){
        //빈 자리가 없어서 손님이 즉시 퇴장할 때 라이프를 차감하는 용도
        life.GetComponent<Life_Indicator>().Set_Life(false);
    }

    //게임오버 판단용, 최초 실행 시에는 false
    public bool gameover_flag = false;
    public bool gameclear_flag = false;


    public void GameOver(){
        gameover_flag = true;
        //TODO : 씬을 다시 로드하고, 위에 게임 오버 텍스트를 출력
        //게임오버 텍스트는 플래그가 트루일때만 눈에 보여야 한다
        SceneManager.LoadScene("Diner");
        gameclear_text.SetActive(false);
    }

    public void Game_Clear(){
        for(int i = 0; i < counters.Count; i++){
            //카운터에 손님이 없어야 함
            if(counters[i].GetComponent<OrderWP_flag>().guest != null 
            || counters[i].GetComponent<OrderWP_flag>().flag == true){
                return;
            }
        }
        gameclear_flag = true;
        //TODO : 씬을 다시 로드하고, 위에 게임 클리어 텍스트를 출력
        //게임 클리어 텍스트는 플래그가 트루일때만 눈에 보여야 한다
        SceneManager.LoadScene("Diner");
        gameover_text.SetActive(false);
    }

    public void Game_Reset(){
        //게임을 재시작할 때 사용
        //라이프 초기화
        life_now = life_init;
        //게임오버 플래그 초기화
        gameover_flag = false;
        //TODO : 씬을 다시 로드하고, 게임오버 텍스트를 숨긴다
    }

    void Update(){
        if(gameover_flag){
            gameover_text.SetActive(true);
        } else {
            gameover_text.SetActive(false);
        }
        if(gameclear_flag){
            gameclear_text.SetActive(true);
        } else {
            gameclear_text.SetActive(false);
        }
    }
}
