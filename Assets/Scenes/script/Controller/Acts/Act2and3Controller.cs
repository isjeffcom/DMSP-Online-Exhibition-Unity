using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Act2and3Controller : MonoBehaviour
{
    public static Act2and3Controller _ins;
    
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
        
        EnterAct3();
        MissionController._ins.CompelteAvailable(false);
        
      
    }

    public void EnterAct3()
    {
        MainController._ins.ToAct(3);
        NPCsController._ins.NPCswitch(3);//npc need to be active before MaptoAct3()
        MainController._ins.MapToAct3();

        MissionController._ins.MissionText(3);
        MissionController._ins.MissionContent(3);

        ColliderController._ins.SwitchCollider(3);
    }


}
