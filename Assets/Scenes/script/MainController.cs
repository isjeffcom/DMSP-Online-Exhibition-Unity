using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController _ins;


    // Global Vars
    public static int _act = 1; 

    private void Awake()
    {
        _ins = this;
    }


    public void ToAct(int toAct)
    {
        _act = toAct;
    }
}
