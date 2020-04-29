using UnityEngine;

public class Buttons : MonoBehaviour
{

    public void ToNextUI(string name)
    {
        UIController._ins.switchUIView(name, true);
        //Act1and2Controller._ins.EnterAct1();
    }

    public void CloseItemWindow()
    {
        ImageViewerController._ins.CloseImage();
    }

    public void LastImage()
    {
        Debug.Log("a");
        ImageViewerController._ins.LastImage();
    }

    public void NextImage()
    {
        ImageViewerController._ins.NextImage();
    }

    public void Complete()
    {
        MissionController._ins.CompleteClick();
    }

    public void toActs(int act)
    {
        if (act == 2)
        {
            Act1and2Controller._ins.EnterAct2();
        }
        if (act == 3)
        {
            Act2and3Controller._ins.EnterAct3();
        }
        if (act == 4)
        {
            Act3and4Controller._ins.EnterAct4();
        }
    }
}
