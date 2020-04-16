using UnityEngine;

public class OnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIController._ins.switchUIView("START_SCREEN", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
