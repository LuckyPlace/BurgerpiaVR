using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;

public class Object_Hierarchy : MonoBehaviour
{
    public bool flag = false;
    XRGrabInteractable grab_inter_flag;
    Coroutine setCoord_flag = null;

    GameManager gm;

    void Start()
    {
        grab_inter_flag = this.GetComponent<XRGrabInteractable>();
        grab_inter_flag.selectEntered.AddListener(OnGrab);
        grab_inter_flag.selectExited.AddListener(OnRelease);
        flag = false;
        gm = GameManager.pub_ins;
    }

    public void flag_switch(int n)
    {
        flag = n > 0;
    }

    void Update()
    {
        if (setCoord_flag == null)
        {
            setCoord_flag = StartCoroutine(Set_Coord());
        }

        if (flag)
        {
            grab_inter_flag.enabled = true;
        }
        else
        {
            if (transform.parent != null)
                grab_inter_flag.enabled = false;
        }
    }

    IEnumerator Set_Coord()
    {
        Debug.Log(this.gameObject.name + "의 좌표 재설정 시작");
        yield return new WaitForSeconds(0.1f);

        if (this.transform.parent != null && this.transform.parent.tag == "Ingredient")
        {
            float height = transform.parent.GetComponent<BoxCollider>().size.y;
            height += transform.parent.GetComponent<BoxCollider>().center.y * 2;
            height += GetComponent<BoxCollider>().center.y * 2;
            transform.localPosition = new Vector3(0, height + 0.03f, 0);
            this.transform.rotation = transform.parent.rotation;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag("Ingredient"))
            {
                float height = transform.parent.GetComponent<BoxCollider>().size.y;
                height += transform.parent.GetComponent<BoxCollider>().center.y * 2;
                height += GetComponent<BoxCollider>().center.y * 2;
                transform.localPosition = new Vector3(0, height + 0.03f, 0);
                child.rotation = transform.rotation;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (flag)
        {
            if (collision.gameObject.tag != "Ingredient") return;

            if (transform.position.y < collision.transform.position.y)
            {
                collision.rigidbody.velocity = Vector3.zero;
                GetComponent<Rigidbody>().velocity = Vector3.zero;

                Vector3 p_position = transform.position;
                Vector3 c_position = collision.transform.position;
                c_position.x = p_position.x;
                c_position.y = p_position.y + GetComponent<Collider>().bounds.size.y + 0.01f;
                c_position.z = p_position.z;

                transform.position = p_position;
                collision.transform.position = c_position;

                collision.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);

                Rigidbody rb = GetComponent<Rigidbody>();
                Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();

                if (otherRb != null) otherRb.isKinematic = true;

                collision.gameObject.GetComponent<XRGrabInteractable>().throwOnDetach = false;
                collision.gameObject.GetComponent<XRGrabInteractable>().enabled = false;

                collision.gameObject.transform.parent = transform;

                rb.isKinematic = false;

                if (transform.childCount > GetComponent<Ing_Code>().init_child)
                {
                    flag_switch(0);
                }
                else
                {
                    flag_switch(1);
                }
            }
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (setCoord_flag != null)
        {
            StopCoroutine(setCoord_flag);
            setCoord_flag = null;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (setCoord_flag == null)
        {
            setCoord_flag = StartCoroutine(Set_Coord());
        }
    }
}
