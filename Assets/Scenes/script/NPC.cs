using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    // UI Prefabs
    [SerializeField]
    private Text textPrefab = null;

    // Get Tip Text
    private Text tip;

    // Get Dialog container
    private GameObject dialogCont;
    private GameObject dialogAns;
    private Text textAns;

    private void Awake()
    {
        // Find tip
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();
        // Find container
        dialogCont = GameObject.Find("UI_Dialog_Cont");
        dialogAns = GameObject.Find("UI_Dialog_Answer");
        textAns = dialogAns.GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
             

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        tip.text = "E to chat";

        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("e");
            //clear tip
            tip.text = "";
            // Display dialog container
            dialogCont.SetActive(true);
            dialogAns.GetComponent<Text>().text = "Hello";
         

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DialogController._ins.CloseDialog();
    }

    // Creates a textbox showing the the line of text
    //void CreateContentView(string text)
    //{
    //    myText = Instantiate(textPrefab) as Text;
    //    myText.text = text;
    //    myText.transform.SetParent(dialogCont.transform, false);
    //}

    public void showDialogAnswer()
    {
        DialogController._ins.ShowDialog(this.name, textAns.text);
    }
}
