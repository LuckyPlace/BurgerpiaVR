using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Vector3 pivot;
    public float doorOpenAngle = -90.0f;            //�� ���� ����
    public float doorCloseAngle = 0.0f;             //�� ���� ����
    public float smooth = 4.0f;                     //������ �ӵ�
    public bool open = false;                       //������ ���� �����ϴ� flag����

    void Update()
    {
        if (open)
        {
            Quaternion targetRotationOpen = Quaternion.Euler(0, doorOpenAngle, 0);      //���� ���� ���� ���� ����
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);       //�ڿ������� �� ����
        }
        else
        {
            Quaternion targetRotationClose = Quaternion.Euler(0, doorCloseAngle, 0);    //���� ���� ���� ���� ����
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClose, smooth * Time.deltaTime);      //�ڿ������� �� ����
        }
    }
}
