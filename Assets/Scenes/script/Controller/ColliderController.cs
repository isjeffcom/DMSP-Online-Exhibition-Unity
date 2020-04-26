using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public static ColliderController _ins;

    GameObject Collider_Act1;
    GameObject Collider_Act3;
    GameObject Collider_Act4;

    GameObject Colliders;
    private List<GameObject> AllColliders = new List<GameObject>();

    private void Awake()
    {
        _ins = this;

        Collider_Act1 = GameObject.Find("Collider_Act1");
        Collider_Act3 = GameObject.Find("Collider_Act3");
        Collider_Act4 = GameObject.Find("Collider_Act4");

        Colliders = GameObject.Find("Colliders");

        GetAllColliders();
    }

    private void GetAllColliders()
    {

        foreach (Transform child in Colliders.transform)
        {
            AllColliders.Add(child.gameObject);
        }
    }

    //Switch colliders for acts
    public void SwitchCollider(int act)
    {

        foreach (GameObject item in AllColliders)
        {
            item.SetActive(false);
        }
        switch (act)
        {
            case 1:
                Collider_Act1.SetActive(true);
                break;
            case 2:
                Collider_Act1.SetActive(true);
                break;
            case 3:
                Collider_Act3.SetActive(true);
                break;
            case 4:
                Collider_Act4.SetActive(true);
                break;
        }
    }

}
