using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stacker : MonoBehaviour
{
    //오브젝트가 현재 부모를 가지고 있는 지 여부, 코드 축약을 위해 사용
    public bool isStacked = false;
    //재료가 처음부터 바로 충돌해서 쌓이는 걸 방지
    public bool flag = false;

    void Start(){
        //자체 회전 비활성화
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnCollisionEnter(Collision collision){
        if(flag){
            //둘 중 더 아래에 있는 재료를 부모로 설정
            if(transform.position.y < collision.transform.position.y){
                //재료 끼리 충돌하고, 아래에 올 재료가 다른 재료를 자식으로 가지지 않았다면
                if(collision.gameObject.tag == "Ingredient"
                && GetComponent<Ing_Code>().init_child == transform.childCount){
                    collision.transform.parent = transform;
                    collision.gameObject.GetComponent<Stacker>().isStacked = true;
                    //자식 오브젝트의 그랩 비활성화
                    collision.transform.GetComponent<XRGrabInteractable>().enabled = false;
                    //물리 동기화
                    RigidSync(collision.transform, transform);
                }
            }
        }

    }

    private void RigidSync(Transform child, Transform parent){
        //부모와 자식의 물리를 동기화
        Rigidbody parentRb = parent.GetComponent<Rigidbody>();
        Rigidbody childRb = child.GetComponent<Rigidbody>();
        if (parentRb != null && childRb != null){
            // 자식의 Rigidbody를 부모와 동기화
            childRb.isKinematic = true;
            childRb.useGravity = false;

            // 부모의 물리적 특성을 자식에게 적용
            parentRb.velocity = Vector3.zero;
            childRb.velocity = parentRb.velocity;
            parentRb.angularVelocity = Vector3.zero;
            childRb.angularVelocity = parentRb.angularVelocity;

            Sync_PosandRtt(child.gameObject, parent.gameObject);
        }
    }

    private void FixedUpdate() {
        if(flag){
            //회전 초기화
            transform.rotation = Quaternion.identity;
            //내 밑에 재료가 있다면
            if(isStacked){
                //밑에 있는 재료와 회전 동기화
                if(transform.parent != null){
                    transform.localRotation = Quaternion.identity;
                }
            }
        }

    }

    void Sync_PosandRtt(GameObject child, GameObject parent){
        float p_height = parent.GetComponent<BoxCollider>().bounds.extents.y * 2;
        p_height += parent.GetComponent<BoxCollider>().center.y;
        child.transform.position = parent.transform.position + new Vector3(0, p_height + 0.01f, 0);
    }
}
