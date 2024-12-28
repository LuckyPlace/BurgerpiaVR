using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Ing_Enum;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Pouring_Sauce : MonoBehaviour
{
    Dictionary<GameObject, GameObject> saucable = new Dictionary<GameObject, GameObject>();
    public List<GameObject> saucable_ing = new List<GameObject>();
    //public GameObject sauceEffect;

    private void Start()
    {
        Set_Dict();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Saucable>() != null)
        {
            Ing_List reference = other.gameObject.GetComponent<Saucable>().refer;
            GameObject refer_obj = Resources.Load<GameObject>("Project/Prefab/Ingredient/" + reference.ToString());
            if (saucable.ContainsKey(refer_obj))
            {
                other.gameObject.GetComponent<Saucable>().isValid = true;
            }
        }
    }

    void Set_Dict()
    {
        for(int i = 0; i < saucable_ing.Count; i++)
        {
            saucable.Add(saucable_ing[i], saucable_ing[i]);
        }
    }
}
