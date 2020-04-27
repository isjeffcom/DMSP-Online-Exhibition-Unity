using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Act3and4Controller : MonoBehaviour
{
    public static Act3and4Controller _ins;
    
    private void Awake()
    {
        _ins = this;
        
    }

    private void Start()
    {
        
    }

    public void ActCheck()
    {
        
        AudioController._ins.StopAudio();
        
        EnterAct4();
        MissionController._ins.CompelteAvailable(false);
        
      
    }

    public void EnterAct4()
    {
        MainController._ins.ToAct(4);
        NPCsController._ins.NPCswitch(4);//npc need to be active before MaptoAct3()
        MainController._ins.MapToAct4();

        MissionController._ins.MissionText(4);
        MissionController._ins.MissionContent(4);

        ColliderController._ins.SwitchCollider(4);
    }


}
