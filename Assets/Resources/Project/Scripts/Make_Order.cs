using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ing_Enum;

public class Make_Order : MonoBehaviour
{
    //난이도 조절을 위한 스테이지 카운터
    //public GameObject stageCounter;
    public int level;
    public List<Ing_List> order = new List<Ing_List>();
    // Start is called before the first frame update
    void Start()
    {
        Add_Ing();
    }

    void Add_Ing()
    {
        var values = System.Enum.GetValues(typeof(Ing_List));
        int count = level; // 햄버거는 최대 8개의 재료를 가질 수 있으며, 레벨에 따라 수가 달라짐

        int bunL_num = UnityEngine.Random.Range(0, 5); //밑에 오는 빵은 5종류
        int bunU_num = UnityEngine.Random.Range(5, 11); //위에 오는 빵은 6종류

        //리스트의 맨 처음에는 밑에 오는 빵이 있어야 하고,
        order.Add((Ing_List)bunL_num);

        //빵 사이에 오는 재료들 추가
        for (int i = 0; i < count; i++)
        {
            Ing_List randomIngredient = (Ing_List)values.GetValue(UnityEngine.Random.Range(11, values.Length));
            order.Add(randomIngredient);
        }

        //리스트의 맨 마지막에는 위에 오는 빵이 있어야 한다
        order.Add((Ing_List)bunU_num);
    }


}
