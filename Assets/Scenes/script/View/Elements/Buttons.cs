using UnityEngine;

public class Buttons : MonoBehaviour
{

    public void ToNextUI(string name)
    {
        UIController._ins.switchUIView(name, true);
    }

    public void CloseItemWindow()
    {
        ItemController._ins.CloseItemDetail();
    }
}
