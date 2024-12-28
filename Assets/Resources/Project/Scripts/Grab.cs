using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grab : MonoBehaviour
{
    GameManager gm;
    public XRGrabInteractable grabInteractable;
    public GameObject otherBurger;                  //다른 버거
    public bool isStart = false;                    //true면 Start버거 false면 Exit버거
    void Start()
    {
        gm = GameManager.pub_ins;
        //게임을 정지시켜 버리면 Grab도 못해서 아주 느리게 시간이 흐르도록 설정
        if (isStart == true)
        {
            Time.timeScale = 0.01f;
        }
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
        else
        {
            Debug.Log("GrabInteractable is null");
        }
    }

    void OnDestory()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (isStart == true)
        {
            StartGame();
        }
        else
        {
            ExitGame();
        }
        otherBurger.SetActive(false);
        gameObject.SetActive(false);
    }

    void ExitGame()
    {
        Debug.Log("Game Exited!");
        //게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        //#if UNITY_EDITOR 전처리기를 사용하여 Unity 에디터에서 실행 중인 경우
        //UnityEditor.EditorApplication.isPlaying = false;를 통해 에디터 플레이 모드를 종료합니다.
        //Application.Quit();를 통해 실제 빌드된 애플리케이션을 종료합니다.
    }

    void StartGame()
    {
        Debug.Log("Game Started!");
        //게임 시작
        Time.timeScale = 1f;
        //게임 초기화
        gm.Game_Reset();
    }
}
