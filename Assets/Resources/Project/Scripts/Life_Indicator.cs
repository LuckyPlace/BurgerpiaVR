using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Life_Indicator : MonoBehaviour
{
    GameManager gm;
    int life_num;
    public int lifeCount;
    public GameObject[] light;
    public TextMeshPro gameOver;

    void Start(){
        gm = GameManager.pub_ins;
        life_num = gm.life_init;
        lifeCount = gm.life_now;
        Life_Init();
    }

    void Update(){
        lifeCount = gm.life_now;
        if(lifeCount < 0) gm.GameOver();
        //gameover flag가 true일 때 게임오버 텍스트 활성화
        if(gm.gameover_flag == true)
        {
            gameOver.gameObject.SetActive(true);
        }
    }

    private void Life_Init(){
        for(int i = 0; i <= life_num; i++){
            light[i].SetActive(true);
        }
    }

    public void Set_Life(bool flag){
        //주문이 실패하면 라이프를 차감
        if(!flag){
            Debug.Log("lifeCount: " + lifeCount);
            light[lifeCount].SetActive(false);
            gm.life_now--;
        }
    }
}
