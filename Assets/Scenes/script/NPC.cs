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

    // Flag for if dialog window opened
    private bool dialogEnabled = false;

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
        if(collision.name == "player")
        {
            tip.text = "E to chat";
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            // Check if dialog has already enabled
            if (Input.GetKey(KeyCode.E) && !dialogEnabled)
            {
                //clear tip
                tip.text = "";


                // Display dialog container
                showDialogAnswer();
                //dialogAns.GetComponent<Text>().text = "Hello";
                dialogEnabled = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DialogController._ins.CloseDialog();
        dialogEnabled = false;
        tip.text = "";
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
        DialogController._ins.ShowDialog(this.name, 0);
    }
}
