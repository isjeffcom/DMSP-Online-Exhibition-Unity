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
}
