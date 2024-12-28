using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Vector3 pivot;
    public float doorOpenAngle = -90.0f;            //문 열릴 각도
    public float doorCloseAngle = 0.0f;             //문 닫힐 각도
    public float smooth = 4.0f;                     //열리는 속도
    public bool open = false;                       //열릴지 말지 결정하는 flag변수

    void Update()
    {
        if (open)
        {
            Quaternion targetRotationOpen = Quaternion.Euler(0, doorOpenAngle, 0);      //문이 열릴 때의 각도 설정
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);       //자연스럽게 문 열림
        }
        else
        {
            Quaternion targetRotationClose = Quaternion.Euler(0, doorCloseAngle, 0);    //문이 닫힐 때의 각도 설정
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClose, smooth * Time.deltaTime);      //자연스럽게 문 닫힘
        }
    }
}
